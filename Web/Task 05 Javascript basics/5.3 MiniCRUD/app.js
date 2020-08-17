function generateID() {
    return '_' + Math.random().toString(36).substr(2, 9);
}

class Service {
    constructor() {
        this.map = new Map();
    }

    add(obj) {
        if (obj == null || obj == undefined)
            return null;

        let id = generateID();
        this.map.set(id, obj);
        return id;
    }

    getById(id) {
        if (id == null || id == undefined)
            return null;

        return this.map.has(id) ? this.map.get(id) : null;
    }


    getAll() {
        return this.map.values();
    }

    deleteById(id) {
        if (id == null || id == undefined)
            return null;

        if (this.map.has(id)) {
            let obj = this.map.get(id);
            this.map.delete(id);
            return obj;
        }

        return null;
    }

    updateById(id, obj) {
        if (id == null || id == undefined || obj == null || obj == undefined)
            return;

        if (this.map.has(id)) {
            for (let prop in obj) {
                this.map.get(id)[prop] = obj[prop];
            }
        } else {
            this.map.set(id, obj);
        }
    }

    replaceById(id, obj) {
        if (id == null || id == undefined || obj == null || obj == undefined)
            return;

        if (this.map.has(id))
            this.map.delete(id);

        this.map.set(id, obj);
    }
}

function outGetAll(storage) {
    console.log('Get all:')
    console.log(storage.getAll());
    console.log('------------------');
}

let storage = new Service();

let catId = storage.add({ 'name': 'mrCat', 'type': 'siberian' });
let cat2Id = storage.add({ 'name': 'vaska', 'type': 'mongrel' });
let pigId = storage.add({ 'name': 'nuff-nuff', 'sayString': 'HRUE-HRUE-HRUE' });
let numberId = storage.add(12345678);

console.log('Map: ');
console.log(storage.map);
console.log('------------------');

outGetAll(storage);

console.log('Get by id cat vaska:');
console.log(storage.getById(cat2Id));
console.log('------------------');

outGetAll(storage);

console.log('Delete by id cat vaska:');
console.log(storage.deleteById(cat2Id));
console.log('------------------');

outGetAll(storage);

console.log('Update by id pig name:');
console.log(storage.updateById(pigId, { 'name': 'niff-niff' }));
console.log('------------------');

outGetAll(storage);

console.log('Replace by id number:');
console.log(storage.replaceById(numberId, { 'someText': 'Now i am an object' }));
console.log('------------------');

outGetAll(storage);

console.log('Try to call replace by id without parameters:')
console.log(storage.replaceById());
console.log('------------------');

outGetAll(storage);

console.log('Map: ');
console.log(storage.map);
console.log('------------------');