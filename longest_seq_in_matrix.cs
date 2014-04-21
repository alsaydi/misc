void Main()
{
	int [][] matrix = new int[][]{ new int[]{1,0,1,0,0}
					   ,new int[]{1,0,0,0,0}
					   ,new int[]{1,1,1,1,0}
					   ,new int[]{1,0,1,0,0}
					   ,new int[]{1,0,1,1,1}
					};
	PrintMatrix(matrix);
	Console.WriteLine();
	FindLongestSequence(matrix);
}
static void PrintMatrix(int [][] matrix){
	for(int i=0;i<matrix.Length;i++){
		for(int j=0;j<matrix[i].Length;j++){
			Console.Write("{0} ",matrix[i][j]);
		}
		Console.WriteLine();
	}
}
static void FindLongestSequence(int [][] matrix){
	int max_row_seq = 0;
	int max_col_seq = 0;
	int curr_row_seq = 0;
	int curr_col_seq = 0;
	
	int row_seq_row = -1;	
	int row_seq_end_col = -1;
	
	int col_seq_col = -1;
	int col_seq_end_row = -1;
	for(int i=0;i<matrix.Length;i++){
		curr_row_seq = 0;
		curr_col_seq = 0;
		for(int j=0;j<matrix[i].Length;j++){
			if(matrix[i][j] == 1){
				curr_row_seq ++;
			}else{
				curr_row_seq = 0;			
			}
			if(curr_row_seq>max_row_seq){
				max_row_seq = curr_row_seq;
				row_seq_row = i;
				row_seq_end_col = j;
			}
			if(matrix[j][i] == 1){	
				curr_col_seq++;
			}else{
				curr_col_seq=0;
			}
			if(curr_col_seq>max_col_seq){
				max_col_seq = curr_col_seq;
				col_seq_col = i;
				col_seq_end_row = j;
			}
		}
	}
	Console.WriteLine("Longest row seq: {0}",max_row_seq);
	Console.WriteLine("Longest row row: {0}",row_seq_row);
	for(int j=row_seq_end_col;j>=0 && max_row_seq-->0;j--){
		Console.Write("{0} ",matrix[row_seq_row][j]);
	}
	Console.WriteLine();
	
	Console.WriteLine("Longest col seq: {0}",max_col_seq);
	Console.WriteLine("Longest col col: {0}",col_seq_col);
	for(int i=col_seq_end_row;i>=0 && max_col_seq-->0;i--){
		Console.Write("{0} ",matrix[i][col_seq_col]);
	}
	Console.WriteLine();
}
// Define other methods and classes here
