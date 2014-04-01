void Main()
{
	//WL(Math.Log(256,16).ToString());	
	/*
	int n = int.Parse("FFFE7960",System.Globalization.NumberStyles.AllowHexSpecifier);
	WL(""+n);
	hex_div_approach(n);
	WL(hex(n));
	WL(bit_approach(n));
	//return;	
	*/
	for(int i=-10000000;i<=10000000;i++){
		//Console.WriteLine("{0} 0x{1}",i.ToString("X"),hex(i));
		if(int.Parse(bit_approach(i),System.Globalization.NumberStyles.AllowHexSpecifier)!=i){
			WL(hex(i));
			Console.WriteLine("{0} 0x{1} {2}",i.ToString("X"),hex(i),i);
		}	
	}
}
void WL(string s){
	Console.WriteLine(s);
}
string hex(int n){
	string fillerDigit = "0";
	var sign = 1;
	if(n<0){		
		n = int.MinValue - (n*-1);
		fillerDigit = "F";		
		sign = -1;
		//WL(""+n);
		if(n==0){
			return "80000000";
		}
	}
	int place = 0;
	int num = 0;
	int mult = 0;
	string s = "";
	
	int prev_place = 9;
	if(n==0)
		return hexDigits[n];
	while(n>0){
		place = (int)Math.Log(n,16)+1; //(int)(Math.Log(n)/2.77)+1; //
		var diff = prev_place-place-1;
		while(diff-->0){s+=fillerDigit;}
		fillerDigit = "0";
		prev_place = place;
		
		num = (int)Math.Pow(16,place-1);
		mult = (int)n/num;		
		num *= mult;		
		s+= sign<0 && s == ""? hexDigits[mult+8]:hexDigits[mult];//(char)((mult < 10) ? (mult + 48) : (mult + 55)); //
		n -= num;
	}
	while(place-->1){s+="0";}
	return s;
}
void hex_div_approach(int n){
	var q = 0;
	var r = 1;
	var s = "";
	bool negative = false;
	if(n < 0){
		n = int.MinValue - n;
		negative = true;
	}
	n = Math.Abs(n);
	
	while(n>0){
		q = (int)(n / 16);
		r = n % 16;
		s += hexDigits[r];
		n = q;		
	}
	if(negative){
		s = s.Substring(0,s.Length-1)+hexDigits[r+8];
	}
	s = new string(s.Reverse().ToArray());
	WL(s);
}
string bit_approach(int n){
	string s = "";
	int shiftOffset = 28;
	int bits = n;
	int bitMask = 0x0FFFFFFF;
	while(shiftOffset>=0){
		bits = n >> shiftOffset;
		bits &= 0x0000000F;		
		s += hexDigits[bits];		
		shiftOffset -=4;
		n &= bitMask;
		bitMask >>= 4;
	}
	return s;
}
static string[] hexDigits = new string[]{"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F"};

// Define other methods and classes here
