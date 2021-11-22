using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using U2U.Games.Core.Entities;

namespace U2U.GameStore.Web.ViewModels.Games
{
  public class GamesCreateViewModel
  {
    [Required]
    [StringLength(128)]
    [AllowNull]
    public string Name { get; set; }

    public int PublisherId { get; set; }

    public IEnumerable<Publisher>? Publishers { get; set; }
  }
}
