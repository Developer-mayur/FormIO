using FormIOProject.Areas.Identity.Model;
using FormIOProject.Data.Migrations;
using FormIOProject.Models;
using System.Runtime.CompilerServices;

namespace FormIOProject.Areas.Identity.Services
{
    public interface IDapper
    {
        public Task AddFormsTable(FormIO form);
    }
}
