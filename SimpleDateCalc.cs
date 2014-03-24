/**
 * Simple date calcs
 * EP problem 19
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
public class Program{
public static void Main()
{
    Console.Read();
	int day = 7;
	int m = 1;
	int year = 1900;
	List<int> shortMonths = new List<int>{4,6,9,11};
	int sdate = CombineDate(year,m,day);
	int edate = CombineDate(2000,12,31);
    int sundays = 1;
    //int offset = 0;
    int prevDay = day;
    var mdates = new List<DateTime>() { new DateTime(year, m, day) };
	while(sdate <  edate){
      
        prevDay = day;
        day += 7;
		if(day > 28 && m == 2 && !IsLeapYear(year)){
			m ++;
            day = (day - 28);
		}
		else if(day > 29 && m == 2){
			m++;
			day = day - 29 ;
		}
		else if(day > 30 && shortMonths.Contains(m)){
			m++;
			day = day - 30 ;
		}
		else if(day > 31 && m == 12){
			year++;
			m = 1;
			day = day - 31;
		}
        else if (day > 31)
        {
            m++;
            day = day - 31;
        }
        /*else
        {
            day++;
        }*/
        if (day == 0) day = 7;
        sundays++;
        sdate = CombineDate(year, m, day);
        if (new DateTime(year, m, day).DayOfWeek != DayOfWeek.Sunday)
        {
            Console.Error.WriteLine("Wrong {0:D}", new DateTime(year, m, day));
        }
        mdates.Add(new DateTime(year, m, day));
        //Console.WriteLine("{0:D}", new DateTime(year, m, day));
        //Console.WriteLine("{0:G2}/{1:G2}/{2:G4}",m,day,year);		
	};
    Console.WriteLine(sundays);
    DateTime date = new DateTime(1900, 1, 1);
    DateTime date2 = new DateTime(2000, 12, 31);
    sundays = 0;
    var tdates = new List<DateTime>();
    while (date <= date2)
    {
        if (date.DayOfWeek == DayOfWeek.Sunday)
        {
            tdates.Add(date);
            sundays++;
        }
        date = date.AddDays(1);
    }
    
    Console.WriteLine(sundays);
    Console.WriteLine("mdates count {0}", mdates.Count);
    Console.WriteLine("tdates count {0}", tdates.Count);
    var ndates = tdates.Except(mdates);
    foreach (var d in ndates)
    {
        Console.WriteLine("{0:D}", d);
    }
}
static bool IsLeapYear(int year){
	if(year % 4 > 0)
		return false;
	if(year % 100 > 0)
		return true;
	if(year % 400 == 0)
		return true;
	return false;
}
static int CombineDate(int year,int m,int day){
	return (year * 100 + m) * 100 + day;
}

}
