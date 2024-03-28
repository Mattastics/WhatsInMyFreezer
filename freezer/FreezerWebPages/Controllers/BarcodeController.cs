//using IronBarCode;
//using Microsoft.AspNetCore.Mvc;

//namespace FreezerWebPages.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class BarcodeController : ControllerBase
//    {
//        private static string _lastSavedImagePath;
//        private readonly FoodDataService _foodDataService;

//        public BarcodeController(FoodDataService foodDataService)
//        {
//            _foodDataService = foodDataService;
//        }

//        [HttpPost("scan")]
//        public async Task<IActionResult> Scan([FromBody] BarcodeRequest request)
//        {
//            try
//            {
//                if (request == null || string.IsNullOrWhiteSpace(request.ImageData))
//                {
//                    return BadRequest("Invalid image data");
//                }

//                var base64Data = request.ImageData.Replace("data:image/png;base64,", string.Empty);
//                var imageBytes = Convert.FromBase64String(base64Data);

//                // Ensure the SavedImages directory exists
//                var savedImagesPath = Path.Combine(Directory.GetCurrentDirectory(), "SavedImages");
//                if (!Directory.Exists(savedImagesPath))
//                {
//                    Directory.CreateDirectory(savedImagesPath);
//                }

//                // Save the image to the server
//                _lastSavedImagePath = Path.Combine(savedImagesPath, Guid.NewGuid().ToString() + ".png");
//                System.IO.File.WriteAllBytes(_lastSavedImagePath, imageBytes);

//                using (var ms = new MemoryStream(imageBytes))
//                {
//                    var options = new BarcodeReaderOptions
//                    {
//                        Speed = ReadingSpeed.Detailed,
//                        ExpectMultipleBarcodes = false,
//                        ExpectBarcodeTypes = BarcodeEncoding.EAN13,
//                        ImageFilters = new ImageFilterCollection()
//                        {
//                            new SharpenFilter(3.5f),
//                            new ContrastFilter(2f)
//                        }
//                    };

//                    var results = BarcodeReader.Read(ms, options);

//                    if (results != null && results.Count > 0)
//                    {
//                        var firstResult = results[0];

//                        if (!string.IsNullOrWhiteSpace(firstResult.Text))
//                        {
//                            var foodItemName = await _foodDataService.GetFoodItemNameByBarcodeAsync(firstResult.Text);
//                            if (foodItemName != null)
//                            {
//                                return Ok(new { name = foodItemName });
//                            }
//                            else
//                            {
//                                return NotFound("Food item not found with the provided barcode.");
//                            }
//                        }
//                    }

//                    return NotFound("Barcode not found.");
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception: {ex.Message}");
//                return StatusCode(500, "Internal server error occurred.");
//            }
//        }

//        [HttpGet("lastimage")]
//        public IActionResult GetLastImage()
//        {
//            if (string.IsNullOrEmpty(_lastSavedImagePath) || !System.IO.File.Exists(_lastSavedImagePath))
//            {
//                return NotFound("No last image found or the image has been deleted.");
//            }

//            var imageBytes = System.IO.File.ReadAllBytes(_lastSavedImagePath);
//            return File(imageBytes, "image/png");
//        }

//        public class BarcodeRequest
//        {
//            public string ImageData { get; set; }
//        }
//    }
//}
