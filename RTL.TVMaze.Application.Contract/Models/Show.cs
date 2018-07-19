namespace RTL.TVMaze.Application.Contract.Models
{
    public class Show
    {        
        public int Id { get; set; }
        public string Name { get; set; }
        public Cast[] Casts { get; set; }
    }
}
