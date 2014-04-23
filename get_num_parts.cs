void Main()
{
	int n = 100000;
	//var ps = GetNumberOfPartitions(n,1);
	var ps = GetNumberOfPartitions(n);
	Console.WriteLine(ps);
	
	//Console.WriteLine(counter);
}
class Key : IEquatable<Key>{
	public Key(int n,int largest){
		this.n = n;
		this.largest = largest;
	}
	private int n;
	private int largest;
	public override int GetHashCode(){ return this.ToString().GetHashCode();}
	private bool IsEqual(Key other){
		//Console.WriteLine(this);
		return this.n == other.n && this.largest == other.largest;
	}
	public override bool Equals(object other){
		if(other == null) return false;
		var obj = other as Key;
		if(obj == null) return false;
		return IsEqual(obj);
	}
	bool IEquatable<Key>.Equals(Key other){
		//Console.WriteLine("test");
		if(other == null) return false;
		return this.IsEqual(other);
	}
	public override string ToString(){
		return string.Format("{0},{1}",this.n,this.largest);
	}
	
};
Dictionary<Key,BigInteger > cache = new Dictionary<Key,BigInteger >();
BigInteger  GetNumberOfPartitions(int n, int largest){
	if(n == 0){
		return 1;
	}
	BigInteger  sum = 0;
	var key = new Key(n,largest);
	if(cache.ContainsKey(key)) return cache[key];
	for(int i=largest;i<=n;i++){
		if(n == largest){
			sum += 1;
		}
		if(n > largest){
			key= new Key(n-i,i);
			if(cache.ContainsKey(key)) sum += cache[key];
			else{
				var temp = GetNumberOfPartitions(n-i,i);
				cache[new Key(n-i,i)]=temp;
				sum += temp;
			}			
		}
	}
	return sum;
}
//this approach is adapted from a solution to euler31
//it's very fast.
BigInteger GetNumberOfPartitions(int n){
	BigInteger [] arr = new BigInteger[n+1];
	for(int i=0;i<arr.Length;i++){
		arr[i] = 0;
	}
	arr[0]=1;
	for(int i=1;i<=n;i++){
		for(int j=i;j<=n;j++){
			arr[j] = arr[j] + arr[j - i];
		}
	}
	return arr[n];
}
