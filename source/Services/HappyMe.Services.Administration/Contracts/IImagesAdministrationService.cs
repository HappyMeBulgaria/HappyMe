using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyMe.Data.Models;

namespace HappyMe.Services.Administration.Contracts
{
    public interface IImagesAdministrationService : IAdministrationService<Image>
    {
        Image Create(byte[] imageByteArray, string authorId);
    }
}
