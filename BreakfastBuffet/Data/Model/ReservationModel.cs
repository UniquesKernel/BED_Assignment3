using MessagePack;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BreakfastBuffet.Data.Model;

public class ReservationModel
{
  public int RoomNumber { get; set; }
  public int ReservationsAdult { get; set; }
  public int ReservationsChild { get; set; }
  public int AttendingAdults { get; set; }
  public int AttendingChildren { get; set; }
  public DateTime ReservationDate { get; set; }


}