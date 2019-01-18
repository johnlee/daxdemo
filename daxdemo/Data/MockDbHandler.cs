using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace daxdemo.Data
{
    public class MockDbHandler : IDbHandler
    {
        private List<Widget> _mockData;

        public MockDbHandler()
        {
            _mockData = new List<Widget>();
            GenerateMockData(100); // TODO
        }

        public async Task<List<Widget>> Read()
        {
            await Task.Delay(10);
            return _mockData;
        }

        public async Task Write(Widget widget)
        {
            await Task.Delay(10);
            _mockData.Add(widget);
        }

        private void GenerateMockData(int count)
        {
            string[] pks = new string[] { "WIDGET10", "WIDGET20", "WIDGET30", "WIDGET40", "WIDGET50" };
            Random r = new Random();

            for (int i = 0; i < count; i++)
            {
                int index = r.Next(pks.Length);
                _mockData.Add(new Widget()
                {
                    pk = pks[index],
                    sk = RandomDate(r),
                    data = RandomString(r)
                });
            }
        }

        private static string RandomDate(Random r)
        {
            DateTime startDate = new DateTime(2018, 1, 1);
            int range = (DateTime.Today - startDate).Days;
            return startDate.AddDays(r.Next(range)).ToString();
        }

        private static string RandomString(Random r)
        {
            const string chars = "abcdefghijklmnopqrstuvwxy";
            int length = r.Next(4, 7);
            return new string(Enumerable.Repeat(chars, length).Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}
