### Demostração de desenvolvimento de Api Minimal com SqlLite


A ideia demostrar de form a simples a criação de   uma api minimal , neste exemplo utilizo Net Core 8 EntityFrameWorke Core.

Criei uma classe chamada produto

<div style = "display : inline_block"></br>
  public class Produto
  {
      public Guid Id { get; set; }

      [MaxLength(100)]
      public string Nome { get; set; } = string.Empty;

      [Column(TypeName = "decimal(10,2)")] 
      public decimal Preco { get; set; }

      public int Estoque { get; set; }
  }


  Utilizando Scalforder criei a classe ProdutoEndpoints onde ira conter todos nossos end-point .








</div>
