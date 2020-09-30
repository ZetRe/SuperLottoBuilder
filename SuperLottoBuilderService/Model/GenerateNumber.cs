using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SuperLottoBuilderService.Model
{
    public class GenerateNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Period { get; set; }

        [MaxLength(50)]
        public string Code { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
