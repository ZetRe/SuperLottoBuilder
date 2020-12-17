using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperLottoBuilderService.Model;

namespace SuperLottoBuilderService.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    public class GenerateController : ControllerBase
    {
        //private readonly string _selectArray = new string[35] {"01","02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "02", "01", "02", "01", "02", "01", "02", "01", "02", };
        private readonly SuperLottoContext _context;

        public GenerateController(SuperLottoContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<List<string>> GenerateSelectionNumbers(string Period, int Nums)
        {
            var list = new List<string>();
            for (int k = 0; k < Nums; k++)
            {
              
                list.Add(GetCode());
            }
            this._context.GenerateNumbers.AddRange(list.Select(a => new GenerateNumber()
            {
                Code = a,
                CreateTime = DateTime.Now,
                Period = Period
            }));
            var result = await this._context.SaveChangesAsync();

            if (result > 0)
            {
                return list;
            }
            throw new Exception("存库失败");
        }


        [HttpPost]
        public async Task<List<string>> GetSelectionNumbers(string Period)
        {
            return await this._context.GenerateNumbers.Where(a => a.Period == Period).Select(a=>a.Code).ToListAsync();
        }

        [HttpPost]
        public async Task<WinningView> WinningConfim(string Period,string Code)
        {
            var winning = await this._context.WinningNumbers.FirstOrDefaultAsync(a => a.Period == Period);
            if(winning==null)
            {
                this._context.WinningNumbers.Add(new WinningNumber() { 
                 Period=Period, Code=Code
                });
                await this._context.SaveChangesAsync();
            }
            var selectNumber= await this._context.GenerateNumbers
                .Where(a => a.Period == Period)
                .Select(a => a.Code)
                .ToListAsync();

            var view = new WinningView();
            var first = selectNumber.Where(a => a == Code).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.First, first));
            first.Select(a => selectNumber.Remove(a));

            var two = selectNumber.Where(a => FrontConfim(5, a, Code) && BackConfim(1, a, Code)).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Second, two));
            two.Select(a => selectNumber.Remove(a));

            var th3 = selectNumber.Where(a => FrontConfim(5, a, Code) ).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Third, th3));
            th3.Select(a => selectNumber.Remove(a));

            var th4 = selectNumber.Where(a => FrontConfim(4, a, Code) && BackConfim(2,a,Code)).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Fourth, th4));
            th4.Select(a => selectNumber.Remove(a));

            var th5 = selectNumber.Where(a => FrontConfim(4, a, Code) && BackConfim(1, a, Code)).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Fifth, th5));
            th5.Select(a => selectNumber.Remove(a));

            var th6 = selectNumber.Where(a => FrontConfim(3, a, Code) && BackConfim(2, a, Code)).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Six, th6));
            th6.Select(a => selectNumber.Remove(a));



            var th7 = selectNumber.Where(a => FrontConfim(4, a, Code)).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Seven, th7));
            th7.Select(a => selectNumber.Remove(a));


            var th8 = selectNumber.Where(a => (FrontConfim(3, a, Code) && BackConfim(1, a, Code))|| (FrontConfim(2, a, Code) && BackConfim(2, a, Code))).ToList();
            view.WinningCodes.Add(new KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Eight, th8));
            th8.Select(a => selectNumber.Remove(a));


            var th9 = selectNumber.Where(a => FrontConfim(3, a, Code) 
            || (FrontConfim(1, a, Code) && BackConfim(2, a, Code))
            || (FrontConfim(2, a, Code) && BackConfim(1, a, Code))
            || BackConfim(2, a, Code)).ToList();
            view.WinningCodes.Add(new  KeyValuePair<Enum.Bonus, List<string>>(Enum.Bonus.Nine, th9));
            th9.Select(a => selectNumber.Remove(a));

            return view;
        }

        [HttpPost]
        public async Task<List<WinningView>> Simulate(int begin,int end,int num)
        {
            var list = new List<WinningView>();
            for (int i=begin;i<end; i++)
            {
                await this.GenerateSelectionNumbers(i.ToString(),num);
                list.Add(await this.WinningConfim(i.ToString(), GetCode()));
            }
            return list;
        }

        [HttpPost]
        public  async Task<List<string>> GetWinningNumber (int count)
        {
            var list = new List<string>();
            while(list.Count<count)
            {
                var number = GetCode();
                for (int i = 0; i < 1000000; i++)
                {
                    var code = GetCode();
                    if (code == number)
                    {
                        list.Add(number);
                        break;
                    }
                }
            }
            return list;
        }
         
        private static string GetCode()
        {
            var arr = new string[7];
            for (int i = 0; i < 7; i++)
            {
                bool isContinue = true;
                while (isContinue)
                {
                    Random random = new Random(Guid.NewGuid().GetHashCode());
                    string randomResult = "";
                    if (i < 5)
                    {
                        randomResult = random.Next(1, 35).ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        randomResult = random.Next(1, 12).ToString().PadLeft(2, '0');
                    }
                    if (!arr.Contains(randomResult))
                    {
                        arr[i] = randomResult;
                        isContinue = false;
                    }
                }

            }
            var array = arr.Take(5).OrderBy(a => a).ToList();
            array.AddRange(arr.Skip(5).OrderBy(a => a));
            return string.Join(" ", array);
        }

        private static  bool FrontConfim(int count,string selectCode,string winningCode)
        {
            var selectfront = selectCode.Split(' ').Take(5);
            var winningfront= winningCode.Split(' ').Take(5);
            return selectfront.Intersect(winningfront).Count() == count;
        }

        private static bool BackConfim(int count, string selectCode, string winningCode)
        {
            var selectfront = selectCode.Split(' ').Skip(5);
            var winningfront = winningCode.Split(' ').Skip(5);
            return selectfront.Intersect(winningfront).Count() == count;
        }




    }
}
