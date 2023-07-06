
// Find the 2-digit year (00-99) with the most dates that
// when month and day are multiplied equal the year.
// e.g. 06/02/12 => 6 x 2 = 12
// and  01/06/06 => 1 x 6 = 6

// How many days in each month (index 1 = January)
var daysInMonths = new List<int>() { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31, };

// We only need to look at days that are less than:
//      - the number of days in the month OR
//      - the lowest factor of the month and 99 (since that's how many years we're looking at)
// ...whichever is smallest.
Func<int, int> getMaxDays = (m) =>
{
    var maxMultiple = (int)Math.Floor(99f / (float)m);
    return Math.Min(daysInMonths[m], maxMultiple);
};

Enumerable.Range(1, 12)
    .Select(m => getMaxDays(m))
    .SelectMany((max, idx) => Enumerable.Range(1, max)
        .Select(day => new
        {
            Year = day * (idx + 1),
            Month = idx + 1,
            Day = day
        }))
    .GroupBy(date => date.Year)
    .OrderByDescending(g => g.Count())
    .First()
    .ToList()
    .ForEach(date =>
        Console.WriteLine($"{date.Month}/{date.Day}/{date.Year}"));

/* 
    Answer: 24!
    So 2024 will have 7 dates that meet this criterion:
        1/24/24
        2/12/24
        3/8/24
        4/6/24
        6/4/24
        8/3/24
        12/2/24
*/
