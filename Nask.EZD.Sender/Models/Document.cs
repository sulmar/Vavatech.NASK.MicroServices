using Bogus;

namespace Nask.EZD.Sender
{
    public class Document
    {
        public int Id { get; set; }
        public string Number { get; set; }     
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsChecked { get; set; }
        public DocumentType DocumentType { get; set; } 
    }

    public enum DocumentType
    {
        Draft,

        Note,

        Letter
    }


    public class DocumentFaker : Faker<Document>
    {
        public DocumentFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Lorem.Word());
            RuleFor(p => p.Number, f => f.Phone.PhoneNumber());
            RuleFor(p => p.IsDeleted, f => f.Random.Bool(0.8f));
            RuleFor(p => p.DocumentType, f=>f.PickRandom<DocumentType>());
            Ignore(p => p.IsChecked);

        }
    }
}