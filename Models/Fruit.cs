using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FruitsApp.Models
{
    public enum FruitUpkeepDifficulty
    {
        Easy,
        Medium,
        Hard
    }
    public class Fruit
    {
        public long Id { get; set; }
      //  [MaxLength(10, ErrorMessage ="Nume must have at most 10 characters")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public long MarketPrice { get; set; }
        public FruitUpkeepDifficulty FruitUpkeepDifficulty { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
