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