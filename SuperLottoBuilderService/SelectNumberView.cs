using SuperLottoBuilderService.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperLottoBuilderService
{
    public class WinningView
    {
        public int Count1 { get => WinningCodes.FirstOrDefault(a=>a.Key== Bonus.First).Value.Count(); } 

        public int Count2 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Second).Value.Count(); } 
        public int Count3 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Third).Value.Count(); } 
        public int Count4 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Fourth).Value.Count(); } 
        public int Count5 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Fifth).Value.Count(); } 
        public int Count6 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Six).Value.Count(); } 
        public int Count7 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Seven).Value.Count(); } 
        public int Count8 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Eight).Value.Count(); } 
        public int Count9 { get => WinningCodes.FirstOrDefault(a => a.Key == Bonus.Nine).Value.Count(); }

        public List<KeyValuePair<Bonus, List<string>>> WinningCodes { get; set; } = new List<KeyValuePair<Bonus, List<string>>>();

    }

   
}
