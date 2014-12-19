#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>
typedef struct value_index value_index;
typedef struct node_struct{
	int start;
	int end;
	int new_max_idx;
	struct node_struct * next;
	struct node_struct * prev;
};
typedef struct node_struct node;
struct value_index{
	int value;
	int index;
};
int build_segment_tree(int * arr, int start, int end, int * tree, int current_node_idx){
	if (start == end)
	{
		tree[current_node_idx] = arr[start];
		return arr[start];
	}
	int mid = start + (end - start) / 2;
	tree[current_node_idx] = build_segment_tree(arr, start, mid, tree, current_node_idx * 2 + 1)
		+ build_segment_tree(arr, mid + 1, end, tree, current_node_idx * 2 + 2);
	return tree[current_node_idx];
}
int * get_segment_tree(int * arr, int len){
	//http://www.geeksforgeeks.org/segment-tree-set-1-sum-of-given-range/
	int x = (int)(ceil(log2(len))); //Height of segment tree
	int max_size = 2 * (int)pow(2, x) - 1; //Maximum size of segment tree
	int * tree = (int *)malloc(sizeof(int )* max_size);
	build_segment_tree(arr, 0, len - 1,tree, 0);
	return tree;
}
int find_index_of_min(int * arr, int len, int start_index){
	int min = arr[start_index];
	int min_index = start_index;
	int i = 0;
	for (i = start_index; i < len; i++){
		if (arr[i] < min){
			min = arr[i];
			min_index = i;
		}
	}
	return min_index;
}
int find_index_of_max(int * arr, int len, int start_index){
	int i = 0;
	int max = arr[start_index];
	int max_index = start_index;
	for (i = start_index; i<len; i++){
		if (arr[i]>max){
			max = arr[i];
			max_index = i;
		}
	}
	return max_index;
}
void print_array(int * arr, int len){
	int i = 0;
	for (; i < len; i++){
		fprintf(stdout, "%d ", arr[i]);
	}
	fprintf(stdout, "%s\n", "");
}
static node * add_node(node *head, int start, int end, node * tail){
	if (NULL == tail){
		head->next = (node *)malloc(sizeof(node)* 1);
		tail = head->next;
		tail->prev = NULL;
	}
	else{
		node * temp = tail;
		tail->next = (node *)malloc(sizeof(node)* 1);
		tail = tail->next;
		tail->prev = temp;
	}
	tail->start = start;
	tail->end = end;
	tail->new_max_idx = -1;
	tail->next = NULL;
	return tail;
}
node * build_segments(int * arr, int len, node *list, int * num_segments){
	int counter = 0;
	int i = 0;
	node * tail = NULL;
	for (i = 0; i < len; i++){
		int start = i;
		while (i < len - 1 && arr[i] < arr[i + 1]){
			i++;
		}
		tail = add_node(list, start, i, tail);
		counter++;
	}
	*num_segments = counter;
	return tail;
}
void print_segments_r(int * arr, int len, node *tail){
	if (!tail) return;
	struct node_struct * temp = tail;
	int i = 0;
	int j = 0;
	while (temp){
		i = temp->start;
		j = temp->end;
		fprintf(stdout, "%s\t", "");
		for (; i <= j; i++){
			fprintf(stdout, "%d ", arr[i]);
		}
		fprintf(stdout, " ... %d %s\n",temp->new_max_idx>-1? arr[temp->new_max_idx]:-1,"");
		temp = temp->prev;
	}
}

void print_segments(int * arr, int len, node *head){
	if (!head) return;
	struct node_struct * temp = head->next;
	int i = 0;
	int j = 0;
	while (temp){
		i = temp->start;
		j = temp->end;
		fprintf(stdout, "%s\t", "");
		for (; i <= j; i++){
			fprintf(stdout, "%d ", arr[i]);
		}
		fprintf(stdout, "%s\n", "");
		temp = temp->next;
	}
}
void solve(int * arr, int len){
	int i = 0;
	int j = 0;
	int k = 0;
	unsigned long long intervals = 0;
	int current_max = arr[0];
	int max = len;
	int value = 0;
	for (i = 0; i < len; i++){
		value = arr[i];
		current_max = value;
		for (j = i; j < len; j++){
			if (arr[j] < value){
				break;
			}
			if (arr[j] >= current_max){//is_valid_interval(arr,i,j)){
				current_max = arr[j];
				intervals++;
			}
		}
	}
	fprintf(stdout, "naive solution: %llu\n", intervals);
}
unsigned long long sum(unsigned long long n){
	return (n * (n + 1)) / 2;
}
int approximate_binary_search(int * arr, int start, int end, int find){
	int index = -1;
	int mid = start + (end - start) / 2;
	while (start < end){
		if (find>arr[mid]){
			start = mid + 1;
			index = start;
		}
		else if (find < arr[mid]){
			end = mid - 1;
			index = end;
		}
		else{
			index = mid;
			break;
		}
		mid = start + (end - start) / 2;
	}
	return index;
}
void print_node(node * n, int * arr){
	fprintf(stdout, "start: %d v: %d  -  end: %d v: %d\n", n->start, arr[n->start], n->end, arr[n->end]);
}
node * find_segments_with_smaller_min(int *arr, node * head, node * current_node){
	node * temp = head;
	node * list = NULL;
	node * walker = NULL;
	while (temp && temp != current_node){
		walker = (node *)malloc(sizeof(node)* 1);
		walker->start = temp->start;
		walker->end = temp->end;
		walker->next = NULL;
		if (!list)
		{
			list = walker;
		}
		else{
			walker->next = list;
			list = walker;
		}

		temp = temp->next;
	}
	if (list)
	{
		walker = (node *)malloc(sizeof(node)* 1);
		walker->start = 0;
		walker->end = 0;
		walker->next = list;
		list = walker;
	}
	return list;
}
unsigned long long get_numbers_smaller_than(int * arr, int start, int end, int value){
	unsigned long long cnums = end - start + 1;
	if (value<arr[start]) /* smaller than the smallest, nothing smaller*/
	{
		return 0;
	}
	if (value > arr[end]) /* larger than the largest, all smaller*/
	{
		return cnums;
	}
	int idx = approximate_binary_search(arr, start, end, value);
	while (idx >= start && arr[idx] > value) idx--;
	/*while (idx >= start && arr[idx] > value) {
		idx = approximate_binary_search(arr, start, end, --value);
		}*/
	cnums = idx - start + 1;
	/*int j = end;
	while (j>start)
	{
	if (arr[j]>value)
	{
	cnums--;
	j--;
	continue;
	}
	break;
	}*/
	/*if (cnums<0)
	{
	fprintf(stderr, "seg %d %d gave < 0 nums while looking for %d\n", arr[start], arr[end], value);
	}*/
	return cnums;
}
unsigned long long get_numbers_larger_than(int * arr, int start, int end, int value){
	unsigned long long cnums = end - start + 1;
	if (value > arr[end]) /* larger than the largest, nothing larger*/
	{
		return 0;
	}
	if (value<arr[start]) /* smaller than the smallest, all are larger*/
	{
		return cnums;
	}
	int idx = approximate_binary_search(arr, start, end, value);
	while (idx >= start && arr[idx]>value) idx--;
	/*while (idx >= start && arr[idx] > value) {
		idx = approximate_binary_search(arr, start, end, --value);
		}*/
	int smallers = idx - start + 1;
	cnums = cnums - smallers;
	/*int j = start;
	while (j<end)
	{
	if (arr[j]<value)
	{
	cnums--;
	j++;
	continue;
	}
	break;
	}
	/*if (cnums<0)
	{
	fprintf(stderr, "seg %d %d gave < 0 nums while looking for %d\n", arr[start], arr[end], value);
	}*/
	return cnums;
}
unsigned long long count_intervals(int * arr, int len, node * list){
	if (!list)return 0;
	unsigned long long counter = 0;
	unsigned long long mult = 0;

	node * temp = list->next;
	while (temp){
		int min = arr[temp->start];
		int max = arr[temp->end];
		unsigned long long csum = sum(temp->end - temp->start + 1);
		counter += csum;
		node * seg = NULL;
		node * prev_seg = temp;
		seg = temp->prev; //find_segments_with_smaller_min(arr, list->next, temp);
		if (0){
			fprintf(stdout, "%s : ", "Current node");
			print_node(temp, arr);
		//	fprintf(stdout, "counter for S:%d E:%d is %llu\n", arr[temp->start], arr[temp->end], csum);
			print_segments_r(arr, len, seg);
		}
		int heighest_cmax = seg?arr[seg->end]:0;
		while (NULL != seg){
			int cmin = arr[seg->start];
			int cmax = arr[seg->end];
			int new_cmax = seg->new_max_idx > -1 ? arr[seg->new_max_idx] : 0;
			if (new_cmax > cmax) cmax = new_cmax;
			if (cmax > max){
				break;
			}

			if (cmax > heighest_cmax)
			{
				heighest_cmax = cmax;
			}
			if (cmin > min){
				//seg = seg->prev; /* delete this segment since its minimum is greater than current node minimum; impossible for connections to go through */
				prev_seg->prev = seg->prev;
				prev_seg->new_max_idx = seg->end;
				seg = seg->prev;
				continue;
			}
			unsigned long long seg_nums = get_numbers_smaller_than(arr, seg->start, seg->end, min);
			unsigned long long curr_nums = get_numbers_larger_than(arr, temp->start, temp->end, heighest_cmax);
			unsigned long long connections = seg_nums * curr_nums;
			//fprintf(stdout, "Nums from seg (%d , %d) %d, to temp (%d , %d) %d, total %d \n", arr[seg->start], arr[seg->end], seg_nums, arr[temp->start], arr[temp->end], curr_nums,connections);
			mult += connections;
			//counter += mult;
			//fprintf(stdout, "%llu\n", counter);
			min = cmin;
			prev_seg = seg;
			seg = seg->prev;
		}
		temp = temp->next;
	}
	fprintf(stdout, "%llu\n", (unsigned long long)counter + mult);
	//free(sorted_nodes);
	return counter;
}
void remove_prev_pointers_to_prev_segments(int * arr, node * head, node * tail){
	node * last = NULL;
	node * prev = NULL;

	if (!head) return;
	if (!tail) return;
	last = tail;
	prev = last->prev;
	while (prev && last)
	{
		node * ahead = last->next? last->next:  last;
		prev = last->prev;
		if (!prev)
		{
			break;
		}
		if (arr[last->start]<arr[prev->start])
		{
			last->prev = prev->prev;
			if (arr[prev->end]> arr[last->end])
			{
				prev->new_max_idx = last->end;
			}
		}
		last = prev;
	}
}

int main(int argc, char ** argv){
	int N = 0;
	int * arr = NULL;
	int index = 0;
	int i = 0;
	fscanf(stdin, "%d", &N);
	arr = (int *)malloc(sizeof(int)*N);
	memset((void *)arr, 0, sizeof(int)*N);

	while (index < N){
		fscanf(stdin, "%d", arr + index);
		index++;
	}

	int num_segments = 0;
	int logN = (int)log2(N);
	node * head = (node*)malloc(sizeof(node)* 1);
	node * tail = build_segments(arr, N, head, &num_segments);
	//fprintf(stdout, "Number of Segments: %d.\n", num_segments);
	//int * segment_tree = get_segment_tree(arr, N);
	//head = head->next;
	//remove_prev_pointers_to_prev_segments(arr, head, tail);
	/*print_segments(arr, N, head);
	fprintf(stdout, "%s\n", "");
	print_segments_r(arr, N, tail);
	*/
	//fprintf(stdout, "Starting ...\n");
	count_intervals(arr, N, head);
	//fprintf(stdout, "Done.\n");


	//index = approximate_binary_search(arr,0,0,atoi(argv[1]));
	//fprintf(stdout,"%d\n",index);
	//solve(arr, N);
	free(arr);
	//free(segment_tree);
	while (head)
	{
		node * temp = head->next;
		free(head);
		head = temp;
	}
	return 0;
}
