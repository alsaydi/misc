void Main()
{
	int start = 1;
	int end = 100;
	for(int i=start;i<=end;i++){
		var column = ColumnId(i);
		var id = ColumnNumber(column);
		if(id != i){
			Console.WriteLine("ERROR: {0} {1}",i,column);
		}
		Console.WriteLine("{0} {1}",i,column);		
	}
}
string ColumnId(int n){	
	List<int> mults = new List<int>();
	int temp = n;
	int q = 0;
	int r = 0;	
	while(n>0){
		q = n / 26;
		r = n % 26;		
		mults.Add(r);
		n = q;
	}
	mults.Reverse();
	while(mults.LastIndexOf(0)>0){
		for(int i=1;i<mults.Count;i++){
			if(mults[i] == 0){
				mults[i] = 26;			
				mults[i-1]-=1;
			}
		}
	}		
	var s = "";
	for(int i=0;i<mults.Count;i++){			
		if(mults[i]>0)
			s+=letters_27[mults[i]];			
	}	
	return s;
}
int ColumnNumber(string columnId){	
	int ret = 0;
	foreach(char c in columnId){
		ret = ret * 26  +  (int)((c - 'A')+1) ;
	}
	return ret;
}
static char[] letters_27 = {'0','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
