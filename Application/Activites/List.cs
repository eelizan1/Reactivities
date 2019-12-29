using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Activites
{
    public class List
    {
        // list of activities
        public class Query : IRequest<List<Activity>> {} 

        // handler 

        public class Handler : IRequestHandler<Query, List<Activity>>
        {

            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context; 
            }

            public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await _context.Activites.ToListAsync(); 

                return activities; 
            }
        };  
    }
}