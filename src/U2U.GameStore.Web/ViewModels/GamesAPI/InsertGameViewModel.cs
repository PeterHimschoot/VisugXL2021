using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using U2U.Currencies.Core.Entities;

namespace U2U.GameStore.Web.ViewModels.GamesAPI
{
  public class InsertGameViewModel
  {
    [Required]
    [StringLength(128)]
    [AllowNull]
    public string Name { get; set; }

    [Range(minimum: 0.01, maximum: 100)]
    public decimal Amount { get; set; }

    public CurrencyName Currency { get; set; }

    public int PublisherId { get; set; }
  }
}
