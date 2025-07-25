using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity
	{
		private readonly DataContext _context; //construtor

		public GenericRepository(DataContext context) //construtor que recebe o contexto do banco de dados
		{
			_context = context; //recebe o contexto do banco de dados que foi passado na injeção de dependência
		}
		public IQueryable<T> GetAll()
		{
			return _context.Set<T>().AsNoTracking(); //vai a tabela e retorna todos os dados e desliga o rastreamento dar todos os dados
		}
		public async Task<T> GetByIdAsync(int id)
		{
			return await _context.Set<T>()
				.AsNoTracking()
				.FirstOrDefaultAsync(e  =>  e.Id  ==  id); //vai a tabela e retorna o primeiro dado que tiver o id igual ao id passado
		}
		public async Task CreateAsync(T entity)
		{
			await _context.Set<T>().AddAsync(entity); //adiciona o dado na tabela
			await SaveAllAsync(); //salva as mudanças no banco de dados
		}
		public async Task UpdateAsync(T entity)
		{
			_context.Set<T>().Update(entity); //atualiza o dado na tabela
			await SaveAllAsync(); //salva as mudanças no banco de dados
		}
		public async Task DeleteAsync(T entity)
		{
			_context.Set<T>().Remove(entity); //remove o dado da tabela nao é aassincrono pois o remove nao precisa esperar
			await SaveAllAsync(); //salva as mudanças no banco de dados
		}
		public async Task<bool> ExistAsync(int id) //verifica se existe algum dado na tabela com o id passado verifica se existe algum dado na tabela com o id passado
		{
			return await _context.Set<T>().AnyAsync(e => e.Id == id); //verifica se existe algum dado na tabela com o id passado
		}
		private async Task<bool> SaveAllAsync() //task devolve um booleano se conseguiu salvar ou nao
		{
			return await _context.SaveChangesAsync() > 0; //retorna true se conseguiu salvar ou false se nao conseguiu salvar
		}
	}
} 