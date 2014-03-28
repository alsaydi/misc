#include <stdio.h>
#include <stdlib.h>
typedef unsigned long long ulong;
void mult_matrix(ulong **A,ulong ** B,ulong ** AB,int m,int k,int n);
void copy_matrix(ulong **src,ulong **dest,int rows,int cols);
int err_line_counter=0;
ulong fib(ulong n){
  if(n <= 0)
    return 0;
  if(n == 1)
    return 1;
  return fib(n-1)+fib(n-2);
}
ulong fib_itr(ulong n){
  ulong n2 = 0;
  ulong n1 = 0;
  ulong i  = 1;
  ulong fib = 1;
  while(i<n){
    n2 = n1;
    n1 = fib;
    fib = n1 + n2;
    i++;
  }
  return fib;
}


int main(int argc, char ** argv){
 
  int m = 2;
  int k = 2;
  int n = 2;
  ulong **A = (ulong **)malloc(sizeof(ulong *)*m);
  ulong **B  = (ulong **)malloc(sizeof(ulong *)*k);
  ulong **AB  = (ulong **)malloc(sizeof(ulong *)*m);

  ulong i =0;
  ulong j =0;
 
  for( i=0;i<k;i++){
    A[i] = (ulong *)malloc(sizeof(ulong)*k);
  }
  for(i=0;i<n;i++){
    AB[i] = (ulong *)malloc(sizeof(ulong)*n);
    B[i] = (ulong*)malloc(sizeof(ulong)*n);
  }
  A[0][0]=1;
  A[0][1]=1;
  A[1][0]=1;
  A[1][1]=0;

  B[0][0]=1;
  B[0][1]=1;
  B[1][0]=1;
  B[1][1]=0;
  
  AB[0][0]=1;
  AB[0][1]=1;
  AB[1][0]=1;
  AB[1][1]=0;

  ulong npower = 5;
  if(argc>1){
    npower = atoi(argv[1]);
  } 
  int temp = npower;
  npower--;//to match the theorem
  while(npower>0){
    if(npower %2 == 0){
      mult_matrix(B,B,AB,m,k,n);
      copy_matrix(AB,B,m,k);
      npower /= 2;
    }else{
      mult_matrix(A,B,AB,m,k,n);
      copy_matrix(AB,A,m,k);
      npower--;
    }    
  }
  /* the above compared to the recursive solution
     is extremely fast; even the below less optimized version is fast */
  /* for example it generates fib(60) in a fraction of a second
     whereas the recursive is taking minutes so far and no end in sight */
  /* while(npower>1){
    mult_matrix(A,B,AB,m,k,n);
    copy_matrix(AB,A,m,k);
    npower--;
    } */

  
  for(i=0;i<m;i++){
    for(j=0;j<n;j++){
      fprintf(stdout,"%llu ", A[i][j]);
    }
    fprintf(stdout,"%s\n","");
  }
  ulong f = 0;
  f = fib_itr(temp);
  fprintf(stdout,"iterative %llu\n",f);
  
  f = 0;
  //f = fib(temp);
  fprintf(stdout,"recursive %llu\n",f);
  
  
  for( i=0;i<m;i++){
    free(A[i]);
    free(AB[i]);
  }
  for(i=0;i<k;i++){
    free(B[i]);
  }
  free(A);
  free(B);
  free(AB);
  
  return 0;
}
void mult_matrix(ulong **A,ulong ** B,ulong ** AB,int a_rows
		 , int a_cols,int b_cols){
  int i =0;
  int j = 0;
  int k = 0;
  for(i=0;i<a_rows;i++){
    for(j=0;j<b_cols;j++){
      AB[i][j] = 0;
      for(k=0;k<a_cols;k++){
	//fprintf(stderr,"%d: %llu %llu %llu",err_line_counter++, AB[i][j],A[i][k],B[k][j]);
	AB[i][j] = AB[i][j] + A[i][k] * B[k][j];
	//fprintf(stderr," %llu\n",AB[i][j]);
      }
    }
  }
}
void copy_matrix(ulong **src,ulong ** dest,int rows,int cols){
  ulong i =0;
  ulong j = 0;
  for(i=0;i<rows;i++)
    for(j=0;j<cols;j++)
      dest[i][j]=src[i][j];
}
