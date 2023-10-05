import { Container } from 'semantic-ui-react';
import FlashCardSetDashboard from './pages/flashcards/FlashCardSetDashboard';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <Container style={{marginTop: '7em'}}>
          <FlashCardSetDashboard />
        </Container>
      </header>
    </div>
  );
}

export default App;
