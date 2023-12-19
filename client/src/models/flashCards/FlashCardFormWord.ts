export class FlashCardFormWord {
    id?: string = undefined;
    word: string = '';
    translation: string = '';
    index: number = 0;
    isDeleted: boolean = false;

    constructor(word: string, translation: string, index: number, id?: string) {
        this.word = word;
        this.translation = translation;
        this.index = index;
        this.id = id;
    }

    delete() {
        this.isDeleted = true;        
    }

    undelete() {
        this.isDeleted = false;
    }
}