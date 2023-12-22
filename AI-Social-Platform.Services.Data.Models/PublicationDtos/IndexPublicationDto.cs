using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Social_Platform.Services.Data.Models.PublicationDtos
{
    public class IndexPublicationDto
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PublicationsLeft { get; set; }

        public IEnumerable<PublicationDto> Publications { get; set; } = null!;
    }
}
