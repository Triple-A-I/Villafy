using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using VillaUtility;
using VillaWeb.Models;
using VillaWeb.Models.Dto.Villa;
using VillaWeb.Models.Dto.VillaNumber;
using VillaWeb.Models.VM;
using VillaWeb.Services.IServices;

namespace VillaWeb.Controllers
{

    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;

        private readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDto> list = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<VillaNumberDto>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberCreateVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                villaNumberCreateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)).Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.VillaId.ToString()
                });
            }


            return View(villaNumberCreateVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var test = await _villaNumberService.GetAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(StaticDetails.SessinToken));
                if (test != null && test.IsSuccess)
                {
                    return Content("Villa No Already exists");

                }

                var response = await _villaNumberService.CreateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(StaticDetails.SessinToken));
                if (response != null && response.IsSuccess || response.ErrorMessages.Count() > 0)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count() > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }
            }

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (resp != null && resp.IsSuccess)
            {
                model.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(resp.Result)).Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.VillaId.ToString()
                }
            );
            }
            return View(model);

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateVillaNumber(int villaNo)
        {
            VillaNumberUpdateVM villaNumberUpdateVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));

                villaNumberUpdateVM.VillaNumber = _mapper.Map<VillaNumberUpdateDto>(model);

            }

            response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                villaNumberUpdateVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>
                    (Convert.ToString(response.Result)).Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.VillaId.ToString()
                });
                return View(villaNumberUpdateVM);
            }
            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVillaNumber(VillaNumberUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.UpdateAsync<APIResponse>(model.VillaNumber, HttpContext.Session.GetString(StaticDetails.SessinToken));
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
                else
                {
                    if (response.ErrorMessages.Count() > 0)
                    {
                        ModelState.AddModelError("ErrorMessages", response.ErrorMessages.FirstOrDefault());
                    }
                }

            }

            var resp = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (resp != null && resp.IsSuccess)
            {
                List<VillaDto> villaDtos = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(resp.Result));
                model.VillaList = villaDtos.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.VillaId.ToString()
                });

            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteVillaNumber(int villaNo)
        {

            VillaNumberDeleteVM villaNumberDeleteVM = new();
            var response = await _villaNumberService.GetAsync<APIResponse>(villaNo, HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                VillaNumberDto model = JsonConvert.DeserializeObject<VillaNumberDto>(Convert.ToString(response.Result));
                villaNumberDeleteVM.VillaNumber = _mapper.Map<VillaNumberDto>(model);
            }

            response = await _villaService.GetAllAsync<APIResponse>(HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                villaNumberDeleteVM.VillaList = JsonConvert.DeserializeObject<List<VillaDto>>(Convert.ToString(response.Result)).Select(u =>
                new SelectListItem
                {
                    Text = u.Name,
                    Value = u.VillaId.ToString()
                });
                return View(villaNumberDeleteVM);
            }

            return NotFound();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVillaNumber(VillaNumberDeleteVM model)
        {

            var response = await _villaNumberService.DeleteAsync<APIResponse>(model.VillaNumber.VillaNo, HttpContext.Session.GetString(StaticDetails.SessinToken));
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVillaNumber));
            }

            return View(model);
        }
    }
}