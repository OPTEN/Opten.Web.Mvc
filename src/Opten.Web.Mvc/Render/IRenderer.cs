namespace Opten.Web.Mvc.Render
{
	/// <summary>
	/// A Renderer.
	/// </summary>
	public interface IRenderer
	{

		/// <summary>
		/// Trims the output
		/// </summary>
		bool TrimOutput { get; }

		/// <summary>
		/// Returns a <see cref="System.String" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		string ToString();

	}
}