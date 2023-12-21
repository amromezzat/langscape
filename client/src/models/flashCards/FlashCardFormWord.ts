export class FlashCardFormWord {
    id?: string = undefined;
    word: string = '';
    translation: string = '';
    position: number = 0;
    isDeleted: boolean = false;

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