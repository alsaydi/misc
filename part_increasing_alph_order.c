#include <stdio.h>
#include <stdlib.h>
typedef unsigned long long ullong;
void print_array(int * array,int index,int n){
  int sum = 0;
  for(int i=0;i<=index;i++){
    fprintf(stdout,"%d ",array[i]);
    sum += array[i];
  }
  if(sum != n){
    fprintf(stdout,"Sum != n sum == %d, n == %d\n",sum,n);
    exit(-1);
  }
  fprintf(stdout,"%s\n","");
}
ullong part_increasing_alph_order(int n){
  int * array = (int *)malloc(sizeof(int)*n);
  int i =0;
  int partLen = n -1;
  int index = partLen;
  int sum = 0;
  int repeats = 0;
  ullong counter = 1;
  for(i=0;i<n;i++){
    array[i]=1;
  }
  //print_array(array,index,n);
  while( array[0]<n){
    counter ++;
    index = partLen - 1;
    array[index]++;
    sum = n+1;//+1 because we incremented array[index];
    sum -= array[index+1];
    array[index+1]--;    
    i = 1;
    /* after incrementing the number at current index and decrementing at index+1, there are two scenarios
       1) we are still increasing, therefore we spread the number at array[index] over the remaining 
          entries, the last one maybe larger.
       2) are decreasing (e.g: 1 2 2 1) we combine the last two (e.g: 1 2 3).
       note that we used to detect which scenario we are in and branch accordingly
       but we don't need that because of the way we are tracking sum */
    while(1){
      array[index+i]=array[index];
      if(sum + array[index]>n) break;
      sum += array[index];
      i++;
    }
    index = index+i-1;
    array[index] += (n - sum); /*if not increasing anymore, combin the last two elements and if increasing, the last element will be larger. */
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
  ullong solutions = part_increasing_alph_order(n);  
  fprintf(stdout,"Number of solutions: %llu\n",solutions);
  return 0;
}
