﻿using System.Web.Mvc;

namespace AmplaData.Web.Controllers
{
    /// <summary>
    ///     Interface for Repository operations for the model 
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IRepositoryController<in TModel> : IReadOnlyRepositoryController<TModel>
    {
        /// <summary>
        ///     GET /{Model}/Create
        /// </summary>
        /// <returns></returns>
        ActionResult Create();

        /// <summary>
        ///     POST /{Model}/Create
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        ActionResult Create(TModel model);

        /// <summary>
        ///     Get /{Model}/Edit/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult Edit(int id = 0);

        /// <summary>
        ///     POST /{Model}/Edit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        ActionResult Edit(TModel model);

        /// <summary>
        ///     GET /{Model}/Delete/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult Delete(int id = 0);

        /// <summary>
        ///     POST /{Model}/Delete/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        ActionResult DeleteConfirmed(int id);
        
        /// <summary>
        ///     GET /{Model}/History/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult History(int id = 0);

        /// <summary>
        ///     GET /{Model}/Versions/id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ActionResult Versions(int id = 0);
    }
}