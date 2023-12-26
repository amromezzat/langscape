import { FlashCardFormWord } from "./FlashCardFormWord";
import { FlashCardSet } from "./flashCardSet";

export class FlashCardSetForm {
    id?: string = undefined;
    name: string = '';
    words: FlashCardFormWord[] = [];

    constructor(setForm?: FlashCardSet) {
        if(setForm) {
          this.id = setForm.id;
          this.name = setForm.name;
          this.words = setForm.words.map((word, index) => {
            return {
              ...word, 
              updated: word.position !== index, 
              position: index
            } as FlashCardFormWord
          });
        }
    }
}