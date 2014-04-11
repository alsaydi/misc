#include <stdio.h>
#include <stdlib.h>
#include <memory.h>
void partition_iterative(int n);
void print_array(int * array,int length,int n){
	int i = 0;
	int sum = 0;
	for(i=0;i<length;i++){
		//fprintf(stdout,"%d ",array[i]);
		sum += array[i];
	}
	//fprintf(stdout,"%s\n","");
	if(sum != n){
		fprintf(stderr,"Sum %d != n %d\n",sum,n);
		exit(1);
	}
}
char * get_array(int * array,int length){
	int i =0;
	char * s = (char *) malloc(sizeof(char)*1001);
	char * temp = s;
	memset(s,0,sizeof(char));
	for(;i<length;i++){
		sprintf(temp++,"%d",array[i]);
		sprintf(temp++,"%s"," ");

	}
	return s;
}
int main(int argc,char ** argv){
	int n = 5;
	if(argc > 1){
		n = atoi(argv[1]);
	}
	partition_iterative(n);
	return 0;
}
void partition_iterative(int n){
	int * arr = (int *)malloc(sizeof(int)*n);
	int i = 0;
	int partLen = n-1;
	int index = -1;
	int counter = 0;
	int sum = 0;
	for(i=0;i<n;i++){
		arr[i] = 1;
	}
	while(partLen>0)
	{
		index = partLen - 1;
		counter++;
		print_array(arr,partLen+1,n);
		while( ! (index == 0 || arr[index-1]>arr[index]))
		{
			index--;
		}
		arr[index] = arr[index]+1;
		sum = 0;
		for(i =index+1;i<=partLen;i++){
			sum = sum + arr[i];
		}
		for(i=1;i<=sum;i++){
			arr[index+i] = 1;
		}	
		partLen = index+sum - 1;
	}
	fprintf(stdout,"Number of solutions: %d\n",counter);
	free(arr);
}
