class Service {
  #storage = [];

  add(obj) {
    if (this.getById(obj.id)) return null;
    if (typeof obj !== 'object') return null;
    if (Array.isArray(obj)) return null;
    if (typeof obj.id !== 'string') return null;
    this.#storage.push(obj);
  }

  getById = (id) => this.#storage.find((el) => el.id === id) || null;

  getAll = () => this.#storage;

  deleteById(id) {
    const idx = this.#storage.findIndex((obj) => obj.id === id);
    let obj = null;
    if (idx !== -1) {
      obj = this.#storage[idx];
      this.#storage.splice(idx, 1);
    }
    return obj;
  }

  updateById(id, obj) {
    const oldObj = this.getById(id);
    if (oldObj) {
      const keys = Object.keys(oldObj);
      for (let i = 0; i < keys.length; i++) {
        if (obj[keys[i]] && keys[i] !== 'id') {
          oldObj[keys[i]] = obj[keys[i]];
        }
      }
    } else return null;
  }

  replaceById(id, obj) {
    this.deleteById(id);
    this.add(obj);
  }
}

const obj = {
  id: '1',
  name: 'Vasya',
  job: 'Front-end',
};

const obj1 = {
  id: '2',
  name: 'Petya',
  job: 'Back-end',
};

const storage = new Service();

storage.add(obj);
storage.add(obj1);
storage.getById('1');
storage.getAll();
storage.deleteById('1');
storage.updateById('2', obj);
storage.replaceById('2', {
  id: '3',
  name: 'Artem',
  job: 'Full-stack',
});
