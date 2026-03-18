using DigitalAssetManagementSystem.dao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DigitalAssetManagementSystem.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IAssetManagementService _svc;
        public ReservationsController(IAssetManagementService svc) => _svc = svc;

        public async Task<IActionResult> Index()
        {
            var list = await _svc.GetReservationsAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Assets = new SelectList(await _svc.GetAssetsAsync(), "AssetId", "Name");
            ViewBag.Employees = new SelectList(await _svc.GetEmployeesAsync(), "EmployeeId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int assetId, int employeeId, DateTime reservationDate, DateTime startDate, DateTime endDate)
        {
            try
            {
                await _svc.ReserveAssetAsync(assetId, employeeId, reservationDate, startDate, endDate);
                TempData["Msg"] = "Reservation created.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Err"] = ex.Message;
                ViewBag.Assets = new SelectList(await _svc.GetAssetsAsync(), "AssetId", "Name", assetId);
                ViewBag.Employees = new SelectList(await _svc.GetEmployeesAsync(), "EmployeeId", "Name", employeeId);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(int reservationId)
        {
            await _svc.WithdrawReservationAsync(reservationId);
            TempData["Msg"] = "Reservation withdrawn.";
            return RedirectToAction(nameof(Index));
        }
    }
}