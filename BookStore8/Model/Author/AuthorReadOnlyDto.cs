using System.ComponentModel.DataAnnotations;

   namespace BookStore8.Model.Author

{
    public class AuthorReadOnlyDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
    }
}    
