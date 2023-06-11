using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VillaUtility;
using VillaWeb.Models;
using VillaWeb.Models.Dto.Villa;
using VillaWeb.Services.IServices;

namespace VillaWeb.Controllers
{

    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;

        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDto> list = new();
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        //[Authorize(Roles = "admin")]
        public IActionResult CreateVilla()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]

        public async Task<IActionResult> CreateVilla(VillaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model, HttpContext.Session.GetString(StaticDetails.SessinToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Created Successfully";
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            TempData["error"] = "Error Encountered.";

            return View(model);

        }

        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {

                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));

                return View(model);

            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteVilla(VillaDto villaDto)
        {

            var response = await _villaService.DeleteAsync<APIResponse>(villaDto.VillaId, HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa Deleted Successfully";

                return RedirectToAction(nameof(IndexVilla));
            }

            TempData["error"] = "Error Encountered.";

            return View(villaDto);

        }


        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateVilla(int villaId)
        {
            var response = await _villaService.GetAsync<APIResponse>(villaId, HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                VillaDto model = JsonConvert.DeserializeObject<VillaDto>(Convert.ToString(response.Result));

                return View(_mapper.Map<VillaUpdateDto>(model));

            }
            return NotFound();
        }

        //[Authorize(Roles = "admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDto villaUpdateDto)
        {

            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(villaUpdateDto, HttpContext.Session.GetString(StaticDetails.SessinToken));
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Updated Successfully";

                    return RedirectToAction(nameof(IndexVilla));
                }
            }

            TempData["error"] = "Error Encountered.";

            return View(villaUpdateDto);

        }


    }
}