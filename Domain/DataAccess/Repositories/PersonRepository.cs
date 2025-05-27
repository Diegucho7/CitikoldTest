using Microsoft.EntityFrameworkCore;
using RetailCitikold.Domain.DataAccess.Context;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos.Response;

namespace RetailCitikold.Domain.DataAccess.Repositories;

public class PersonRepository(RetailCitikoldDbContext  context) : IPersonService
{
    public async Task<PersonResponseTotalDto> ReadPersonTotal()
    {
        
            var person = await context.Person.ToListAsync();
            if (person == null || person.Count == 0)
            {
                return new PersonResponseTotalDto
                {
                  
                    Person = default
                };
            }
            return new PersonResponseTotalDto
            {
               
                Person = person 
            };
        
    }
}