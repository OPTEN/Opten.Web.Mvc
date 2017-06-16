using System;
using System.Text;

namespace Opten.Web.Mvc
{
	/// <summary>
	/// The Bootstrap Helper.
	/// </summary>
	public class BootstrapHelper
	{

		private readonly int _columnCount;

		/// <summary>
		/// Initializes a new instance of the <see cref="BootstrapHelper"/> class.
		/// </summary>
		/// <param name="columnCount">The column count.</param>
		public BootstrapHelper(int columnCount = 12)
		{
			_columnCount = columnCount;
		}

		/// <summary>
		/// Generates class name.
		/// </summary>
		/// <param name="xsSize">Size of the xs.</param>
		/// <param name="smSize">Size of the sm.</param>
		/// <param name="mdSize">Size of the md.</param>
		/// <param name="lgSize">Size of the lg.</param>
		/// <param name="itemCount">The item count.</param>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public string ColClass(int? xsSize = 12, int? smSize = null, int? mdSize = null, int? lgSize = null, int? itemCount = null, int? index = null)
		{
			var colClass = new StringBuilder();

			ColSizeClass(colClass, xsSize, "xs", itemCount, index);
			ColSizeClass(colClass, smSize, "sm", itemCount, index);
			ColSizeClass(colClass, mdSize, "md", itemCount, index);
			ColSizeClass(colClass, lgSize, "lg", itemCount, index);

			return colClass.ToString().Trim();
		}

		private void ColSizeClass(StringBuilder colClass, int? size, string sizeClass, int? itemCount, int? index)
		{
			if (size.HasValue)
			{
				decimal offset = 0;
				colClass.Append(string.Format(" col-{0}-{1}", sizeClass, size.Value));

				if (itemCount.HasValue && index.HasValue)
				{
					var itemsPerRow = _columnCount / size.Value;
					var positionInRow = index.Value % itemsPerRow;

					// if first element
					if (positionInRow == 0)
					{
						var placeLeftPerRow = _columnCount % size.Value;
						offset = (decimal)placeLeftPerRow / 2;

						var filled = index.Value + itemsPerRow;

						// if row not filled
						if (filled > itemCount.Value)
						{
							offset += ((filled - itemCount.Value) * size.Value) / 2;
						}
					}
				}

				if (offset != 0 || colClass.ToString().Contains("-offset-"))
				{
					colClass.Append(string.Format(" col-{0}-offset-{1}", sizeClass, Math.Round(offset)));
				}
			}
		}
	}
}
