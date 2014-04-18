#include <stdio.h>
#include <stdlib.h>
void print_array(int * array,int index,int n){
  int sum = 0;
  for(int i=0;i<=index;i++){
    // fprintf(stdout,"%d ",array[i]);
    sum += array[i];
  }
  if(sum != n){
    fprintf(stdout,"Sum != n sum == %d, n == %d\n",sum,n);
    exit(-1);
  }
  //  fprintf(stdout,"%s\n","");
}
int part_increasing_alph_order(int n){
  int * array = (int *)malloc(sizeof(int)*n);
  int i =0;
  int partLen = n -1;
  int index = partLen;
  int sum = 0;
  int still_increasing_order = 1;
  int repeats = 0;
  int counter = 1;
  for(i=0;i<n;i++){
    array[i]=1;
  }
  print_array(array,index,n);
  while(partLen >= 0 && array[0]<n){
    counter ++;
    index = partLen - 1;
    array[index]++;
    sum = n+1;//because we incremented array[index];
    for(i=index+1;i<=partLen;i++){
      sum -= array[i];
      array[i]--;
    }
    still_increasing_order = 1;
    for(i=index;i<partLen;i++){
      if(array[i]>array[i+1]){//we are still increasing
	still_increasing_order = 0;
	break;
      }
    }
    if(still_increasing_order == 1){
      repeats = (n - sum) / array[index];
      for(i=1;i<=repeats;i++){
	array[index+i]=array[index];
	sum += array[index];
      }
      index = index+i-1;
      array[index] += (n - sum);
    }else{
      array[index] += array[index+1];
    }
    //print_array(array,index,n);
    partLen = index ;
  }
  free(array);
  return counter;
}
int main(int argc,char ** argv){
  int n = 8;
  if(argc > 1){
    n = atoi(argv[1]);
  }
  int solutions = part_increasing_alph_order(n);  
  fprintf(stdout,"Number of solutions: %d\n",solutions);
  return 0;
}
