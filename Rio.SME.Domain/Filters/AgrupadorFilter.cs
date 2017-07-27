namespace Rio.SME.Domain.Filters
{
    public class AgrupadorFilter : Filter
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public Entities.Agrupador.CampoOrdenacaoAgrupador CampoOrdenacao { get; set; }
    }
}
