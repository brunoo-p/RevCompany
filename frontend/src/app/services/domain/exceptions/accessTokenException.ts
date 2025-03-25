export class AccessTokenException extends Error {

    constructor(msg: string, stack?: string) {

        super(msg);
        this.stack = stack;

        Object.setPrototypeOf(this, AccessTokenException.prototype);
    
    }

}
