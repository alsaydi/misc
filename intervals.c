#include <stdio.h>
#include <string.h>
#include <stdlib.h>
typedef struct node node;
typedef struct value_index value_index;
struct node{
  int start;
  int end;
  int start_value;
  int end_value;
  node * next;
};
struct value_index{
  int value;
  int index;
};
void print_array(int * arr, int len){
  int i=0;
  for(;i<len;i++){
    fprintf(stdout,"%d ",arr[i]);
  }
  fprintf(stdout,"%s\n","");
}
static void add_node(node *head, int start,int end){
  node * temp = head;
  while(temp->next){
    temp = temp->next;
  }
  temp->next = (node *)malloc(sizeof(node)*1);
  temp->next->start = start;
  temp->next->end = end;
  temp->next->next = NULL;
}
void build_segments(int * arr,int len,node *list){
  int i =0;
  for(i=0;i<len;i++){
    int start = i;
    while(i < len-1 && arr[i]<arr[i+1]){
      i++;
    }
    add_node(list,start,i);
   }
}
void print_segments(int * arr, int len , node *head){
  if(!head) return;
  node * temp = head->next;
  int i =0;
  int j =0;
  while(temp){
    i=temp->start;
    j=temp->end;
    for(;i<=j;i++){
      fprintf(stdout,"%d ",arr[i]);
    }
    fprintf(stdout,"%s\n","");
    temp = temp->next;
  }
}
void solve(int * arr, int len){
  int i =0;
  int j =0;
  int k =0;
  int intervals = 0;
  int current_max = arr[0];
  int max = len;
  int value = 0;
  for(i=0;i<len;i++){
    value = arr[i];
    current_max = value;
    for(j=i;j<len;j++){
      if(arr[j]<value){
	break;
      }
      if(arr[j]>=current_max){//is_valid_interval(arr,i,j)){
	current_max = arr[j];
	intervals++;      	
      }      
    }
  }
  fprintf(stdout,"naive solution: %d\n",intervals);
}
int sum(int n){
  return n * (n+1)/2;
}
int approximate_binary_search(int * arr,int start,int end,int find){
  int index = -1;
  int mid = start + (end - start)/2;
  while(start < end){
    if(arr[mid] == find){
      index = mid;
      break;
    }
    if(find>arr[mid]){
      start = mid+1;
      index = start;
    }else if(find < arr[mid]){
      end = mid-1;
      index = end;
    }
    else{
      index = mid;
      break;
    }
    mid = start + (end - start)/2;
  }
  return index;
}
void print_node(node * n){
  fprintf(stdout,"start: %d -  end:%d svalue: %d\n",n->start, n->end,n->start_value);
}
node * find_segments_with_smaller_min(int *arr,node * head, node * current_node){
  node * temp = head;
  node * list = (node *) malloc(sizeof(node *)*1);
  while(temp != current_node){
    if(arr[temp->start]< arr[current_node->start]){
      list->next = (node *) malloc(sizeof(node *)*1);
      list->next->start = temp->start;
      list->next->end = temp->end;
      list->next->next = NULL;
    }
    temp = temp->next;
  }
  list = list->next;
  return list;
}
int less(const void * a, const void *b){
  node * l = (node *)a;
  node * r = (node *)b;
  return l->start_value - r->start_value;
}
int count_intervals(int * arr, int len,node * list){
  if(!list)return 0;
  unsigned long long counter = 0;
  unsigned long long mult = 0;
  node * temp = list->next;
  node * smaller_seg = NULL;
  int prev_max_idx = -1; //index_of_prev_max_in_current_seg
  int curr_min_idx = -1; //index_of_crr_min_in_prev_seg
  int prev_seg_nums = 0;
  int curr_seg_nums = 0;
  int curr_mult = 0;
  node * sorted_nodes = (node * )malloc(sizeof(node *) * len);
  node * seg = NULL;
  int segments = 0;
  int i =0;
/*  while(temp){
    seg = (node *) malloc(sizeof(node *));
    seg->start = temp->start;
    seg->end = temp->end;
    seg->next = temp->next;
    seg->start_value = arr[temp->start];
    sorted_nodes[i++] =  *seg;
    temp = temp->next;
    segments++;
    }
  //qsort(sorted_nodes,i,sizeof(node),less);
  temp = list->next;*/
  i = 0;
  print_node(temp);
  while(temp){
    print_node(temp);
    counter += sum(temp->end - temp->start +1);
    smaller_seg  = find_segments_with_smaller_min(arr,list,temp);
    while(NULL != smaller_seg){
      curr_min_idx = approximate_binary_search(arr,smaller_seg->start,smaller_seg->end,arr[temp->start]);
      fprintf(stdout,"curr min in prev seg %d at %d\n",arr[temp->start],curr_min_idx); 
      if(curr_min_idx > -1 && curr_min_idx > smaller_seg->start){
	prev_max_idx = approximate_binary_search(arr,temp->start,temp->end,arr[smaller_seg->end]);
	if(prev_max_idx>-1){
	  prev_seg_nums = curr_min_idx - smaller_seg->start;
	  curr_seg_nums = temp->end - prev_max_idx+1;
	  
	  //if(mult == 0) mult = 1;
	  curr_mult = prev_seg_nums  *  curr_seg_nums;
	  fprintf(stdout,"curr mult %d for S:%d E:%d max_idx %d, min_idx %d\n",curr_mult,arr[temp->start],arr[temp->end],prev_max_idx,curr_min_idx);
	  mult = mult + curr_mult;
	}
      }
      smaller_seg = smaller_seg->next;
    }
    temp = temp->next;
  }
  fprintf(stdout,"%u\n",counter+mult);
  //free(sorted_nodes);
  return counter;
}
int main(int argc,char ** argv){
  int N = 0;
  int * arr = NULL;
  int index = 0;
  int i = 0;
  fscanf(stdin,"%d",&N);
  arr = (int *)malloc(sizeof(int)*N);
  memset((void *)arr,0,sizeof(int)*N);

  while(index < N){
    fscanf(stdin,"%d",arr+index);
    index++;
  }
  /*  while(i<N){
    fprintf(stderr,"%d ",arr[i++]);
  }
  fprintf(stderr,"%s\n",""); */

  node * head = (node*)malloc(sizeof(node)*1);
  build_segments(arr,N,head);
  count_intervals(arr,N,head);
  print_segments(arr,N,head); 
  //index = approximate_binary_search(arr,0,0,atoi(argv[1]));
  //fprintf(stdout,"%d\n",index);
  solve(arr,N);
  return 0;
}
