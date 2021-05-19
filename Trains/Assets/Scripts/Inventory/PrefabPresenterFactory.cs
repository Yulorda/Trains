using System.Collections.Generic;
using System.Linq;
using UniRx;

    public class PrefabPresenterFactory<T, U> : PrefabComponentFactory<T> where T : PresenterBehaviour<U>
    {
        public IEnumerable<T> Create(List<U> models)
        {
            List<T> results = new List<T>();
            if (models.Count>0)
            {
                results = Create(models.Count).ToList();
                for (int i = 0; i < models.Count; i++)
                {
                    Config(results[i], models[i]);
                }
            }
            return results;
        }

        public T Create(U value)
        {
            var go = Create();
            Config(go, value);
            return go;
        }

        protected virtual void Config(T presenter, U model)
        {
            presenter.InjectModel(model);
        }
    }
