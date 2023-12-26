export class FlashCardFormWord {
    id?: string = undefined;
    word: string = '';
    translation: string = '';
    isDeleted: boolean = false;
    position: number = 0;
    updated: boolean = false;

    constructor(word: string, translation: string, position: number, id?: string) {
        this.word = word;
        this.translation = translation;
        this.position = position;
        this.id = id;
    }

    delete() {
        this.isDeleted = true;        
    }

    undelete() {
        this.isDeleted = false;
    }
}