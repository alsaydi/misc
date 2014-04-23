void Main()
{
	int n = 600;
	var cacheMatrix = new BigInteger[n+1][];
	for(int i=0;i<cacheMatrix.Length;i++){
		cacheMatrix[i] = new BigInteger[n+1];
	}
	cache = cacheMatrix;
	var ps = GetNumberOfPartitions(n,1);
	//var ps = GetNumberOfPartitions(n);
	Console.WriteLine(ps);
	//Console.WriteLine(counter);
}
BigInteger [][] cache;
//recursion with memorization
BigInteger  GetNumberOfPartitions(int n, int largest){
	if(n == 0){
		return 1;
	}
	BigInteger  sum = 0;
	if(cache[n][largest]>0) return cache[n][largest];
	for(int i=largest;i<=n;i++){
		if(n == largest){
			sum += 1;
		}
		if(n > largest){
			if(cache[n-i][i]>0) sum += cache[n-i][i];
			else{
				var temp = GetNumberOfPartitions(n-i,i);
				cache[n-i][i]=temp;
				sum += temp;
			}			
		}
	}
	return sum;
}
//The dynamic programming approach
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
