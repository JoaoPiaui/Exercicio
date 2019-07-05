using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorio
{
    public class AlunoRepositorio
    {
        Conexao conexao = new Conexao();


        public int Inserir(Aluno aluno)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"INSERT INTO alunos (nome, cpf, nota_1, nota_2, nota_3
                                        VALUES (@NOME, @CPF, @NOTA_1, @NOTA_2, @NOTA_3)";
            comando.Parameters.AddWithValue("@NOME", aluno.Nome);
            comando.Parameters.AddWithValue("@CPF", aluno.Cpf);
            comando.Parameters.AddWithValue("@NOTA_1", aluno.Nota1);
            comando.Parameters.AddWithValue("@NOTA_2", aluno.Nota2);
            comando.Parameters.AddWithValue("@NOTA_3", aluno.Nota3);

            int id = Convert.ToInt32(comando.ExecuteScalar());

            comando.Connection.Close();

            return id;
        }

        public List<Aluno> ObterTodos(string busca)
        {
            SqlCommand comando = conexao.Conectar();
            comando.CommandText = @"SELECT * FROM alunos WHERE nome like @BUSCA";

            busca = $"%{busca}%";
            comando.Parameters.AddWithValue("@BUSCA", busca);

            List<Aluno> alunos = new List<Aluno>();
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DataRow linha = tabela.Rows[i];
                Aluno aluno = new Aluno();

                aluno.Id = Convert.ToInt32(linha["id"]);
                aluno.Nome = linha["nome"].ToString();
                aluno.Cpf = linha["cpf"].ToString();
                aluno.Nota1 = Convert.ToDecimal(linha["nota_1"]);
                aluno.Nota2 = Convert.ToDecimal(linha["nota_2"]);
                aluno.Nota3 = Convert.ToDecimal(linha["nota_3"]);
                alunos.Add(aluno);
            }
            return alunos;
        }
    }
}
