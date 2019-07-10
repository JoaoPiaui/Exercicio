using Model;
using Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class AlunoController : Controller
    {
        AlunoRepositorio repositorio = new AlunoRepositorio();

        public ActionResult Index()
        {
            List<Aluno> alunos = repositorio.ObterTodos("");

            ViewBag.Alunos = alunos;

            return View();
        }

        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Store(string nome, string cpf, decimal nota1, decimal nota2, decimal nota3)
        {
            Aluno aluno = new Aluno();
            aluno.Nome = nome;
            aluno.Cpf = cpf;
            aluno.Nota1 = nota1;
            aluno.Nota2 = nota2;
            aluno.Nota3 = nota3;
            int id = repositorio.Inserir(aluno);
            return RedirectToAction("Index");

        }

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
           return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Aluno aluno = repositorio.ObterPeloId(id);
            ViewBag.Aluno = aluno;
            return View();
        }

        public ActionResult Update(int id, string nome, string cpf, decimal nota1, decimal nota2, decimal nota3)
        {
            Aluno aluno = new Aluno();
            aluno.Id = id;
            aluno.Nome = nome;
            aluno.Cpf = cpf;
            aluno.Nota1 = nota1;
            aluno.Nota2 = nota2;
            aluno.Nota3 = nota3;

            bool alterou = repositorio.Atualizar(aluno);
            return RedirectToAction("Index");
        }
    }
}