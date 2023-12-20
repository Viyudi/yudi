using System;

class Arma
{
    public string Nome { get; set; }
    public int Dano { get; set; }

    public Arma(string nome, int dano)
    {
        Nome = nome;
        Dano = dano;
    }
}

class Personagem
{
    public string Nome { get; set; }
    public int PontosVida { get; set; }
    public Arma ArmaEquipada { get; set; }
    private Random random = new Random();

    public Personagem(string nome, int pontosVida, Arma arma)
    {
        Nome = nome;
        PontosVida = pontosVida;
        ArmaEquipada = arma;
    }
    
    public void ReceberDano(int quantidade)
    {
        PontosVida -= quantidade;
        Console.WriteLine($"{Nome} recebe {quantidade} pontos de dano. Pontos de vida restantes: {PontosVida}");
    }

    public void Atacar(Monstro alvo)
    {
        int rolagemDado = random.Next(1, 21); // Números de 1 a 20
        Console.WriteLine($"{Nome} rola um dado: {rolagemDado}");

        if (rolagemDado <= 10) // erra o ataque
        {
            Console.WriteLine($"{Nome} erra o ataque!");
            
        }
        else if(rolagemDado <=15){//acerta
            Console.WriteLine($"{Nome} ataca {alvo.Nome} com {ArmaEquipada.Nome}!");
            alvo.ReceberDano(ArmaEquipada.Dano);
        }
        else
        {
            Console.WriteLine($"{Nome} ataca {alvo.Nome} com {ArmaEquipada.Nome}, o acerto é critico!!");
            alvo.ReceberDano(ArmaEquipada.Dano+5);
        }
    }

    public void Defender(Monstro alvo)
    {
        int rolagemDado = random.Next(1, 21); // Números de 1 a 20
        Console.WriteLine($"{Nome} rola um dado: {rolagemDado}");

        if (rolagemDado <= 10) //nao defende
        {
            Console.WriteLine($"{alvo.Nome} ataca {Nome}!");
            PontosVida -= 5;
            Console.WriteLine($"{Nome} recebe 5 pontos de dano. Pontos de vida restantes: {PontosVida}");
        }
        else{ //defende
           Console.WriteLine($"\n{alvo.Nome} ataca {Nome}, mas {Nome} defende!");
            Console.WriteLine($"{Nome} não receve dano. Pontos de vida restantes: {PontosVida}");
        }
    }
}

class Monstro
{
    public string Nome { get; set; }
    public int PontosVida { get; set; }
    private Random random = new Random();

    public Monstro(string nome, int pontosVida)
    {
        Nome = nome;
        PontosVida = pontosVida;
    }

    public void ReceberDano(int quantidade)
    {
        PontosVida -= quantidade;
        Console.WriteLine($"{Nome} recebe {quantidade} pontos de dano. Pontos de vida restantes: {PontosVida}");
    }

    public void Atacar(Personagem heroi)
    {
        int rolagemDado = random.Next(1, 21); // Números de 1 a 20
        Console.WriteLine($"{Nome} rola um dado: {rolagemDado}");

        if (rolagemDado <= 10) 
        {
            Console.WriteLine($"{Nome} erra o ataque!");
        }
        else if(rolagemDado <=15){//acerta
            Console.WriteLine($"{Nome} ataca {heroi.Nome}!");
            heroi.ReceberDano(10);
        }
        else
        {
            Console.WriteLine($"{Nome} ataca {heroi.Nome}!O acerto é critico!!");
            heroi.ReceberDano(15);
        }
    }
}

class Bonus
{
    public string Nome { get; set; }

    public Bonus(string nome)
    {
        Nome = nome;
    }

    public void AplicarBonus(Personagem heroi)
    {
        switch (Nome.ToLower())
        {
            case "upgrade dano":
                heroi.ArmaEquipada.Dano += 10; 
                Console.WriteLine("Dano da arma aumentado!");
                break;
            case "upgrade vida":
                heroi.PontosVida += 20; 
                Console.WriteLine("Vida máxima aumentada!");
                break;
            case "cura":
                heroi.PontosVida = Math.Min(heroi.PontosVida + 30, 100); 
                Console.WriteLine("Você foi curado!");
                break;
            default:
                Console.WriteLine("Bônus inválido.");
                break;
        }
    }
}



class Program
{
   
    static void Main()
    {
        
        Console.WriteLine("Bem-vindo ao jogo!");
        Personagem heroi = EscolherPersonagem();
        
        Monstro monstro = RandomMonstro();

        Console.WriteLine("\nVocê é um(a) " + heroi.Nome +"!");
        
        Console.WriteLine("\nVocê vai lutar com um " + monstro.Nome + "!");

        while (true)
        {
            Console.WriteLine("\nEscolha uma ação:");
            Console.WriteLine("1. Atacar");
            Console.WriteLine("2. Defender");
            Console.WriteLine("3. Sair");

            string escolha = Console.ReadLine();

            switch (escolha)
            {
                case "1":
                    heroi.Atacar(monstro);
                    if(monstro.PontosVida<=0){
                        Console.WriteLine($"\n{monstro.Nome} foi derrotado!!");
                        
                        Console.WriteLine("Escolha seu bônus:");
                        
                        Bonus bonusEscolhido = EscolherBonus();
                        bonusEscolhido.AplicarBonus(heroi);
                        
                        //escolhe outro monstro
                        monstro = RandomMonstro();
                        Console.WriteLine("\nVocê vai lutar com um " + monstro.Nome + "!");
                    }else{
                        Console.WriteLine($"\n{monstro.Nome} ainda esta vivo!!\n");
                    }
                    
                    if(heroi.PontosVida<=0){
                        Console.WriteLine($"\nVocê foi derrotado!!");
                        heroi = EscolherPersonagem();
                    }else{
                        Console.WriteLine($"\nA batalha ainda não acabou!!\n");
                    }
                    monstro.Atacar(heroi);
                    break;

                case "2":
                    heroi.Defender(monstro);
                    break;
                    
                case "3":
                    Console.WriteLine("Saindo do jogo...");
                    return;

                default:
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                    break;
            }
        }
    }
    static Personagem EscolherPersonagem()
    {
            Console.WriteLine("\nEscolha o seu personagem:");
            Console.WriteLine("1. Cavaleiro");
            Console.WriteLine("2. Mago");
            Console.WriteLine("3. Arqueiro");
    
            string escolhaPersonagem = Console.ReadLine();
    
            switch (escolhaPersonagem)
            {
                case "1":
                    Arma espada = new Arma("Espada", 10);
                    return new Personagem("Cavaleiro", 100, espada);
                case "2":
                    Arma cajado = new Arma("Cajado", 20);
                    return new Personagem("Mago", 80, cajado);
                    break;
                case "3":
                    Arma arco = new Arma("Arco", 15);
                    return new Personagem("Arqueiro", 90, arco);
                    break;
                default:
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                    break;
            }
    
            return null;
    }
    static Monstro RandomMonstro()
    {
       Random random = new Random();
            int randonMonstro =  random.Next(1,4);
            switch (randonMonstro)
            {
                case 1:
                    return new Monstro("Dragão", 90);
                case 2:
                    return new Monstro("Demônio", 80);
                    break;
                case 3:
                    return new Monstro("Ogro", 85);
                    break;
                default:
                    Console.WriteLine("Escolha inválida. Tente novamente.");
                    break;
            }
    
            return null;
    }
    static Bonus EscolherBonus()
    {
    Console.WriteLine("\nEscolha um bônus após derrotar o monstro:");
    Console.WriteLine("1. Upgrade no dano da arma");
    Console.WriteLine("2. Upgrade na vida máxima");
    Console.WriteLine("3. Cura");

    string escolhaBonus = Console.ReadLine();

      switch (escolhaBonus)
      {
          case "1":
              return new Bonus("Upgrade Dano");
          case "2":
              return new Bonus("Upgrade Vida");
          case "3":
              return new Bonus("Cura");
          default:
              Console.WriteLine("Escolha inválida. Tente novamente.");
              return EscolherBonus();
      }
    }
}
