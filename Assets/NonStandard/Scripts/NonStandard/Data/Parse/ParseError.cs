using NonStandard.Extension;
using System.Collections.Generic;

namespace NonStandard.Data.Parse {
	public struct ParseError {
		public int index, row, col;
		public string message;
		public ParseError(int i, int r, int c, string m) { index = i; row = r; col = c; message = m; }
		public ParseError(Token token, IList<int> rows, string m) :this(token.index, rows, m) { }
		public ParseError(int index, IList<int> rows, string m) {
			FilePositionOf(index, rows, out row, out col);
			message = m; this.index = index;
		}
		public override string ToString() {
			return ((index >= 0)?("@" + (row + 1) + "," + col + "(" + index + "): "):"") + message;
		}
		public static ParseError None = default(ParseError);
		public void OffsetBy(int index, IList<int> rows) {
			int r, c; FilePositionOf(index, rows, out r, out c); row += r; col += c;
		}
		public static void FilePositionOf(Token token, IList<int> indexOfRowEnd, out int row, out int col) {
			FilePositionOf(token.index, indexOfRowEnd, out row, out col);
		}
		public static void FilePositionOf(int index, IList<int> indexOfRowEnd, out int row, out int col) {
			if(indexOfRowEnd == null || indexOfRowEnd.Count == 0) { row = 0; col = index; return; }
			row = indexOfRowEnd.BinarySearchIndexOf(index);
			if (row < 0) { row = ~row; }
			int rowStart = row > 0 ? indexOfRowEnd[row - 1] : 0;
			col = index - rowStart;
			if (row == 0) ++col;
		}
		public static string FilePositionOf(Token token, IList<int> indexOfRowEnd) {
			int row, col; FilePositionOf(token, indexOfRowEnd, out row, out col);
			return (row + 1) + "," + (col);
		}
	}
}
