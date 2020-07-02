using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Pages.Product.Components
{
	public partial class ImageUploadBase : ComponentBase
	{
		private List<ImageData> _imagesData;
		ICollection<Models.Image> _modelImages;
		protected IFileListEntry file;



		#region Properties

		[Parameter]
		public List<ImageData> ImagesData
		{
			get
			{
				_imagesData ??= new List<ImageData>();
				return _imagesData;
			}
			set
			{
				if (_imagesData != value && value != null)
				{
					_imagesData = value;
					ImagesDataChanged.InvokeAsync(value);
				}
			}
		}


		[Parameter]
		public ICollection<Models.Image> ModelImages
		{
			get
			{
				_modelImages ??= new List<Models.Image>();
				return _modelImages;
			}
			set
			{
				if (_modelImages != value && value != null)
				{
					_modelImages = value;
					ModelImagesChanged.InvokeAsync(value);
				}

			}
		}


		[Parameter]
		public EventCallback<List<ImageData>> ImagesDataChanged { get; set; }


		[Parameter]
		public EventCallback<ICollection<Models.Image>> ModelImagesChanged { get; set; }

		#endregion


		#region Methods
		//todo: make this method for one or multiple files
		protected async Task<ICollection<Models.Image>> HandleFileSelected(IFileListEntry[] files)
		{
			Models.Image _image = new Models.Image();

			file = files.FirstOrDefault();

			using (MemoryStream ms = new MemoryStream((int)file.Size + 1))
			{
				await file.Data.CopyToAsync(ms);
				_image.Data = ms.ToArray();
			}

			_image.ImageName = file.Name;
			string byte64 = Convert.ToBase64String(_image.Data);
			int extensionIndex = _image.ImageName.IndexOf('.') + 1;
			string extension = _image.ImageName.Remove(0, extensionIndex);

			ImagesData.Add(new ImageData() { name = _image.ImageName, string64Data = new string("data:image/" + extension + "; base64," + byte64) });
			await ImagesDataChanged.InvokeAsync(ImagesData);



			ModelImages.Add(_image);
			_image = new Models.Image();
			return ModelImages;
		}
		#endregion
	}
}
