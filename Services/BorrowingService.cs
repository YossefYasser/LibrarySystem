using EnozomTask.Data;
using EnozomTask.DTO;
using EnozomTask.Interfaces;
using EnozomTask.Models;
using EnozomTask.Models.EnozomTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnozomTask.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly int borrowedStatus;

        public BorrowingService(IBorrowingRepository borrowingRepository)
        {
            _borrowingRepository = borrowingRepository;
            borrowedStatus = 4;
          


            
        }
        public class BookServiceException : Exception
        {
            public BookServiceException(string message) : base(message) { }
        }
        public class BorrowBookServiceException : Exception
        {
            public BorrowBookServiceException(string message) : base(message) { }
        }
        public async Task BorrowBookAsync(BorrowBookDto borrowRequest)
        {
            if (borrowRequest == null || borrowRequest.ExpectedReturnDate == null || borrowRequest.StudentId <= 0 || borrowRequest.copyId <= 0 || borrowRequest.BorrowDate == null)
            {
                throw new ArgumentException("Invalid borrow request. Please ensure all required fields are filled.");
            }

            // Find the book copy that matches the provided book name and is available
            var availableCopy = await _borrowingRepository.GetAvailableCopyAsync(borrowRequest.copyId);
            

            if (availableCopy == null)
            {
                throw new BorrowBookServiceException("This book copy is not available.");
            }

            // Create the borrowing record
            var borrowing = new Borrowing
            {
                StudentId = borrowRequest.StudentId,
                CopyId = availableCopy.Id,
                BorrowDate = borrowRequest.BorrowDate,
                ReturnDate = borrowRequest.ExpectedReturnDate,
                StatusId = borrowedStatus // Assuming 1 corresponds to "Borrowed" status in the CopyStatus table
            };

            // Update the copy status to "Borrowed"
            availableCopy.CopyStatusId = borrowedStatus;

            // Add the borrowing record to the context
            await _borrowingRepository.AddBorrowingAsync(borrowing);
            
        }


       

        public async Task ReturnBookAsync(ReturnBookDto returnBookDto)
        {
            if (returnBookDto == null || returnBookDto.StudentId <= 0 || returnBookDto.borrwingId <= 0)
            {
                throw new ArgumentException("Invalid input.");
            }

            if (returnBookDto.StatusId != 1 && returnBookDto.StatusId != 2 && returnBookDto.StatusId != 3)
            {
                throw new ArgumentException("The provided status is not allowed. Only statuses with ID 1, 2, or 3 are allowed.");
            }

            // Find the borrowing record

            var borrowing = await  _borrowingRepository.GetBorrowingByIdAsync(returnBookDto.borrwingId);
            

            if (borrowing == null)
            {
                throw new Exception("Borrowing record not found or book name mismatch.");
            }

            // Update return date and copy status

            if (borrowing.StatusId != borrowedStatus)
            {
                throw new BookServiceException("You have already returned this book.");
            }
            borrowing.ReturnDate = DateTime.Now;

            // Set the copy status based on the provided status

            await _borrowingRepository.UpdateBorrowingAndCopy(borrowing, returnBookDto.StatusId);



        }
    }
}
