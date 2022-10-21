using System.Security.Cryptography;

namespace NortheastMegabuck.Tests.Squads.Results;

[TestFixture]
internal class Calculator
{
    private NortheastMegabuck.Squads.Results.ICalculator _calculator;

    [OneTimeSetUp]
    public void SetUp()
        => _calculator = new NortheastMegabuck.Squads.Results.Calculator();

    private static NortheastMegabuck.Models.BowlerSquadScore BuildBowlerSquadScore(int game)
        => BuildBowlerSquadScore(Enumerable.Repeat(game, 3).ToArray());

    private static NortheastMegabuck.Models.BowlerSquadScore BuildBowlerSquadScore(params int[] scores)
        => new(scores)
        {
            Bowler = new NortheastMegabuck.Models.Bowler { Id = BowlerId.New() },
        };

    [Test]
    public void NoPreviousAdvancers_NoTies()
    {
        var bowler1 = BuildBowlerSquadScore(40);
        var bowler2 = BuildBowlerSquadScore(39);
        var bowler3 = BuildBowlerSquadScore(38);
        var bowler4 = BuildBowlerSquadScore(37);
        var bowler5 = BuildBowlerSquadScore(36);

        var bowler6 = BuildBowlerSquadScore(35);
        var bowler7 = BuildBowlerSquadScore(34);
        var bowler8 = BuildBowlerSquadScore(33);
        var bowler9 = BuildBowlerSquadScore(32);
        var bowler10 = BuildBowlerSquadScore(31);

        var bowler11 = BuildBowlerSquadScore(30);
        var bowler12 = BuildBowlerSquadScore(29);
        var bowler13 = BuildBowlerSquadScore(28);
        var bowler14 = BuildBowlerSquadScore(27);
        var bowler15 = BuildBowlerSquadScore(26);

        var bowler16 = BuildBowlerSquadScore(25);
        var bowler17 = BuildBowlerSquadScore(24);
        var bowler18 = BuildBowlerSquadScore(23);
        var bowler19 = BuildBowlerSquadScore(22);
        var bowler20 = BuildBowlerSquadScore(21);

        var bowler21 = BuildBowlerSquadScore(20);
        var bowler22 = BuildBowlerSquadScore(19);
        var bowler23 = BuildBowlerSquadScore(18);
        var bowler24 = BuildBowlerSquadScore(17);
        var bowler25 = BuildBowlerSquadScore(16);

        var bowler26 = BuildBowlerSquadScore(15);
        var bowler27 = BuildBowlerSquadScore(14);
        var bowler28 = BuildBowlerSquadScore(13);
        var bowler29 = BuildBowlerSquadScore(12);
        var bowler30 = BuildBowlerSquadScore(11);

        var bowler31 = BuildBowlerSquadScore(10);
        var bowler32 = BuildBowlerSquadScore(9);
        var bowler33 = BuildBowlerSquadScore(8);
        var bowler34 = BuildBowlerSquadScore(7);
        var bowler35 = BuildBowlerSquadScore(6);

        var bowler36 = BuildBowlerSquadScore(5);
        var bowler37 = BuildBowlerSquadScore(4);
        var bowler38 = BuildBowlerSquadScore(3);
        var bowler39 = BuildBowlerSquadScore(2);
        var bowler40 = BuildBowlerSquadScore(1);

        var scores = new[] {bowler1, bowler2, bowler3, bowler4, bowler5,
                            bowler6, bowler7, bowler8, bowler9, bowler10,
                            bowler11, bowler12, bowler13, bowler14, bowler15,
                            bowler16, bowler17, bowler18, bowler19, bowler20,
                            bowler21, bowler22, bowler23, bowler24, bowler25,
                            bowler26, bowler27, bowler28, bowler29, bowler30,
                            bowler31, bowler32, bowler33, bowler34, bowler35,
                            bowler36, bowler37, bowler38, bowler39, bowler40 }.ToList();

        scores.Shuffle();

        //var previousAdvancersIds = new[] { bowler1.Bowler.Id, bowler2.Bowler.Id, bowler3.Bowler.Id, bowler4.Bowler.Id, bowler5.Bowler.Id };

        var cashRatio = 5.0m;
        var finalsRatio = 8.0m;

        var result = _calculator.Execute(SquadId.New(), new NortheastMegabuck.Models.Division(), scores, Enumerable.Empty<BowlerId>(), finalsRatio, cashRatio);

        Assert.Multiple(() =>
        {
            var advancingScores = result.AdvancingScores.ToList();
            Assert.That(advancingScores, Has.Count.EqualTo(5));
            Assert.That(advancingScores[0].Bowler.Id, Is.EqualTo(bowler1.Bowler.Id), "Advancer #1");
            Assert.That(advancingScores[1].Bowler.Id, Is.EqualTo(bowler2.Bowler.Id), "Advancer #2");
            Assert.That(advancingScores[2].Bowler.Id, Is.EqualTo(bowler3.Bowler.Id), "Advancer #3");
            Assert.That(advancingScores[3].Bowler.Id, Is.EqualTo(bowler4.Bowler.Id), "Advancer #4");
            Assert.That(advancingScores[4].Bowler.Id, Is.EqualTo(bowler5.Bowler.Id), "Advancer #5");

            var cashingScores = result.CashingScores.ToList();
            Assert.That(cashingScores, Has.Count.EqualTo(3));
            Assert.That(cashingScores[0].Bowler.Id, Is.EqualTo(bowler6.Bowler.Id), "Casher #1");
            Assert.That(cashingScores[1].Bowler.Id, Is.EqualTo(bowler7.Bowler.Id), "Casher #2");
            Assert.That(cashingScores[2].Bowler.Id, Is.EqualTo(bowler8.Bowler.Id), "Casher #3");

            Assert.That(result.NonQualifyingScores.Count(), Is.EqualTo(32));
        });
    }

    [Test]
    public void NoPreviousAdvancers_TieForLastAdvancer()
    {
        var bowler1 = BuildBowlerSquadScore(140);
        var bowler2 = BuildBowlerSquadScore(139);
        var bowler3 = BuildBowlerSquadScore(138);
        var bowler4 = BuildBowlerSquadScore(137);
        var bowler5 = BuildBowlerSquadScore(35,36,38);

        var bowler6 = BuildBowlerSquadScore(36,36,37);
        var bowler7 = BuildBowlerSquadScore(34);
        var bowler8 = BuildBowlerSquadScore(33);
        var bowler9 = BuildBowlerSquadScore(32);
        var bowler10 = BuildBowlerSquadScore(31);

        var bowler11 = BuildBowlerSquadScore(30);
        var bowler12 = BuildBowlerSquadScore(29);
        var bowler13 = BuildBowlerSquadScore(28);
        var bowler14 = BuildBowlerSquadScore(27);
        var bowler15 = BuildBowlerSquadScore(26);

        var bowler16 = BuildBowlerSquadScore(25);
        var bowler17 = BuildBowlerSquadScore(24);
        var bowler18 = BuildBowlerSquadScore(23);
        var bowler19 = BuildBowlerSquadScore(22);
        var bowler20 = BuildBowlerSquadScore(21);

        var bowler21 = BuildBowlerSquadScore(20);
        var bowler22 = BuildBowlerSquadScore(19);
        var bowler23 = BuildBowlerSquadScore(18);
        var bowler24 = BuildBowlerSquadScore(17);
        var bowler25 = BuildBowlerSquadScore(16);

        var bowler26 = BuildBowlerSquadScore(15);
        var bowler27 = BuildBowlerSquadScore(14);
        var bowler28 = BuildBowlerSquadScore(13);
        var bowler29 = BuildBowlerSquadScore(12);
        var bowler30 = BuildBowlerSquadScore(11);

        var bowler31 = BuildBowlerSquadScore(10);
        var bowler32 = BuildBowlerSquadScore(9);
        var bowler33 = BuildBowlerSquadScore(8);
        var bowler34 = BuildBowlerSquadScore(7);
        var bowler35 = BuildBowlerSquadScore(6);

        var bowler36 = BuildBowlerSquadScore(5);
        var bowler37 = BuildBowlerSquadScore(4);
        var bowler38 = BuildBowlerSquadScore(3);
        var bowler39 = BuildBowlerSquadScore(2);
        var bowler40 = BuildBowlerSquadScore(1);

        var scores = new[] {bowler1, bowler2, bowler3, bowler4, bowler5,
                            bowler6, bowler7, bowler8, bowler9, bowler10,
                            bowler11, bowler12, bowler13, bowler14, bowler15,
                            bowler16, bowler17, bowler18, bowler19, bowler20,
                            bowler21, bowler22, bowler23, bowler24, bowler25,
                            bowler26, bowler27, bowler28, bowler29, bowler30,
                            bowler31, bowler32, bowler33, bowler34, bowler35,
                            bowler36, bowler37, bowler38, bowler39, bowler40 }.ToList();

        scores.Shuffle();

        //var previousAdvancersIds = new[] { bowler1.Bowler.Id, bowler2.Bowler.Id, bowler3.Bowler.Id, bowler4.Bowler.Id, bowler5.Bowler.Id };

        var cashRatio = 5.0m;
        var finalsRatio = 8.0m;

        var result = _calculator.Execute(SquadId.New(), new NortheastMegabuck.Models.Division(), scores, Enumerable.Empty<BowlerId>(), finalsRatio, cashRatio);

        Assert.Multiple(() =>
        {
            var advancingScores = result.AdvancingScores.ToList();
            Assert.That(advancingScores, Has.Count.EqualTo(5));
            Assert.That(advancingScores[0].Bowler.Id, Is.EqualTo(bowler1.Bowler.Id), "Advancer #1");
            Assert.That(advancingScores[1].Bowler.Id, Is.EqualTo(bowler2.Bowler.Id), "Advancer #2");
            Assert.That(advancingScores[2].Bowler.Id, Is.EqualTo(bowler3.Bowler.Id), "Advancer #3");
            Assert.That(advancingScores[3].Bowler.Id, Is.EqualTo(bowler4.Bowler.Id), "Advancer #4");
            Assert.That(advancingScores[4].Bowler.Id, Is.EqualTo(bowler5.Bowler.Id), "Advancer #5");

            var cashingScores = result.CashingScores.ToList();
            Assert.That(cashingScores, Has.Count.EqualTo(3));
            Assert.That(cashingScores[0].Bowler.Id, Is.EqualTo(bowler6.Bowler.Id), "Casher #1");
            Assert.That(cashingScores[1].Bowler.Id, Is.EqualTo(bowler7.Bowler.Id), "Casher #2");
            Assert.That(cashingScores[2].Bowler.Id, Is.EqualTo(bowler8.Bowler.Id), "Casher #3");

            Assert.That(result.NonQualifyingScores.Count(), Is.EqualTo(32));
        });
    }

    [Test]
    public void NoPreviousAdvancers_TieForLastCasher()
    {
        var bowler1 = BuildBowlerSquadScore(140);
        var bowler2 = BuildBowlerSquadScore(139);
        var bowler3 = BuildBowlerSquadScore(138);
        var bowler4 = BuildBowlerSquadScore(137);
        var bowler5 = BuildBowlerSquadScore(136);

        var bowler6 = BuildBowlerSquadScore(135);
        var bowler7 = BuildBowlerSquadScore(134);
        var bowler8 = BuildBowlerSquadScore(133);
        var bowler9 = BuildBowlerSquadScore(132);
        var bowler10 = BuildBowlerSquadScore(30,31,33);

        var bowler11 = BuildBowlerSquadScore(31,31,32);
        var bowler12 = BuildBowlerSquadScore(29);
        var bowler13 = BuildBowlerSquadScore(28);
        var bowler14 = BuildBowlerSquadScore(27);
        var bowler15 = BuildBowlerSquadScore(26);

        var bowler16 = BuildBowlerSquadScore(25);
        var bowler17 = BuildBowlerSquadScore(24);
        var bowler18 = BuildBowlerSquadScore(23);
        var bowler19 = BuildBowlerSquadScore(22);
        var bowler20 = BuildBowlerSquadScore(21);

        var bowler21 = BuildBowlerSquadScore(20);
        var bowler22 = BuildBowlerSquadScore(19);
        var bowler23 = BuildBowlerSquadScore(18);
        var bowler24 = BuildBowlerSquadScore(17);
        var bowler25 = BuildBowlerSquadScore(16);

        var bowler26 = BuildBowlerSquadScore(15);
        var bowler27 = BuildBowlerSquadScore(14);
        var bowler28 = BuildBowlerSquadScore(13);
        var bowler29 = BuildBowlerSquadScore(12);
        var bowler30 = BuildBowlerSquadScore(11);

        var bowler31 = BuildBowlerSquadScore(10);
        var bowler32 = BuildBowlerSquadScore(9);
        var bowler33 = BuildBowlerSquadScore(8);
        var bowler34 = BuildBowlerSquadScore(7);
        var bowler35 = BuildBowlerSquadScore(6);

        var bowler36 = BuildBowlerSquadScore(5);
        var bowler37 = BuildBowlerSquadScore(4);
        var bowler38 = BuildBowlerSquadScore(3);
        var bowler39 = BuildBowlerSquadScore(2);
        var bowler40 = BuildBowlerSquadScore(1);

        var scores = new[] {bowler1, bowler2, bowler3, bowler4, bowler5,
                            bowler6, bowler7, bowler8, bowler9, bowler10,
                            bowler11, bowler12, bowler13, bowler14, bowler15,
                            bowler16, bowler17, bowler18, bowler19, bowler20,
                            bowler21, bowler22, bowler23, bowler24, bowler25,
                            bowler26, bowler27, bowler28, bowler29, bowler30,
                            bowler31, bowler32, bowler33, bowler34, bowler35,
                            bowler36, bowler37, bowler38, bowler39, bowler40 }.ToList();

        scores.Shuffle();

        //var previousAdvancersIds = new[] { bowler1.Bowler.Id, bowler2.Bowler.Id, bowler3.Bowler.Id, bowler4.Bowler.Id, bowler5.Bowler.Id };

        var cashRatio = 5.0m;
        var finalsRatio = 8.0m;

        var result = _calculator.Execute(SquadId.New(), new NortheastMegabuck.Models.Division(), scores, Enumerable.Empty<BowlerId>(), finalsRatio, cashRatio);

        Assert.Multiple(() =>
        {
            var advancingScores = result.AdvancingScores.ToList();
            Assert.That(advancingScores, Has.Count.EqualTo(5));
            Assert.That(advancingScores[0].Bowler.Id, Is.EqualTo(bowler1.Bowler.Id), "Advancer #1");
            Assert.That(advancingScores[1].Bowler.Id, Is.EqualTo(bowler2.Bowler.Id), "Advancer #2");
            Assert.That(advancingScores[2].Bowler.Id, Is.EqualTo(bowler3.Bowler.Id), "Advancer #3");
            Assert.That(advancingScores[3].Bowler.Id, Is.EqualTo(bowler4.Bowler.Id), "Advancer #4");
            Assert.That(advancingScores[4].Bowler.Id, Is.EqualTo(bowler5.Bowler.Id), "Advancer #5");

            var cashingScores = result.CashingScores.ToList();
            Assert.That(cashingScores, Has.Count.EqualTo(3));
            Assert.That(cashingScores[0].Bowler.Id, Is.EqualTo(bowler6.Bowler.Id), "Casher #1");
            Assert.That(cashingScores[1].Bowler.Id, Is.EqualTo(bowler7.Bowler.Id), "Casher #2");
            Assert.That(cashingScores[2].Bowler.Id, Is.EqualTo(bowler8.Bowler.Id), "Casher #3");

            Assert.That(result.NonQualifyingScores.Count(), Is.EqualTo(32));
        });
    }

    [Test]
    public void GitHubIssue9_Situation1()
    {
        var bowlerA = BuildBowlerSquadScore(300);
        var bowlerB = BuildBowlerSquadScore(-1);
        var bowlerC = BuildBowlerSquadScore(-2);
        var bowlerD = BuildBowlerSquadScore(-3);
        var bowlerE = BuildBowlerSquadScore(-4);

        var bowler2 = BuildBowlerSquadScore(35);
        var bowler3 = BuildBowlerSquadScore(34);
        var bowler4 = BuildBowlerSquadScore(33);
        var bowler5 = BuildBowlerSquadScore(32);
        var bowlerF = BuildBowlerSquadScore(31);

        var bowlerG = BuildBowlerSquadScore(30);
        var bowlerH = BuildBowlerSquadScore(29);
        var bowler13 = BuildBowlerSquadScore(28);
        var bowler14 = BuildBowlerSquadScore(27);
        var bowler15 = BuildBowlerSquadScore(26);

        var bowler16 = BuildBowlerSquadScore(25);
        var bowler17 = BuildBowlerSquadScore(24);
        var bowler18 = BuildBowlerSquadScore(23);
        var bowler19 = BuildBowlerSquadScore(22);
        var bowler20 = BuildBowlerSquadScore(21);

        var bowler21 = BuildBowlerSquadScore(20);
        var bowler22 = BuildBowlerSquadScore(19);
        var bowler23 = BuildBowlerSquadScore(18);
        var bowler24 = BuildBowlerSquadScore(17);
        var bowler25 = BuildBowlerSquadScore(16);

        var bowler26 = BuildBowlerSquadScore(15);
        var bowler27 = BuildBowlerSquadScore(14);
        var bowler28 = BuildBowlerSquadScore(13);
        var bowler29 = BuildBowlerSquadScore(12);
        var bowler30 = BuildBowlerSquadScore(11);

        var bowler31 = BuildBowlerSquadScore(10);
        var bowler32 = BuildBowlerSquadScore(9);
        var bowler33 = BuildBowlerSquadScore(8);
        var bowler34 = BuildBowlerSquadScore(7);
        var bowler35 = BuildBowlerSquadScore(6);

        var bowler36 = BuildBowlerSquadScore(5);
        var bowler37 = BuildBowlerSquadScore(4);
        var bowler38 = BuildBowlerSquadScore(3);
        var bowler39 = BuildBowlerSquadScore(2);
        var bowler40 = BuildBowlerSquadScore(1);

        var scores = new[] {bowlerA, bowlerB, bowlerC, bowlerD, bowlerE,
                            bowler2, bowler3, bowler4, bowler5, bowlerF,
                            bowlerG, bowlerH, bowler13, bowler14, bowler15,
                            bowler16, bowler17, bowler18, bowler19, bowler20,
                            bowler21, bowler22, bowler23, bowler24, bowler25,
                            bowler26, bowler27, bowler28, bowler29, bowler30,
                            bowler31, bowler32, bowler33, bowler34, bowler35,
                            bowler36, bowler37, bowler38, bowler39, bowler40 }.ToList();

        scores.Shuffle();

        var previousAdvancersIds = new[] { bowlerA.Bowler.Id, bowlerB.Bowler.Id, bowlerC.Bowler.Id, bowlerD.Bowler.Id, bowlerE.Bowler.Id };

        var cashRatio = 5.0m;
        var finalsRatio = 8.0m;

        var result = _calculator.Execute(SquadId.New(), new NortheastMegabuck.Models.Division(), scores, previousAdvancersIds, finalsRatio, cashRatio);

        Assert.Multiple(() =>
        {
            var advancingScores = result.AdvancingScores.ToList();
            Assert.That(advancingScores, Has.Count.EqualTo(5));
            Assert.That(advancingScores[0].Bowler.Id, Is.EqualTo(bowler2.Bowler.Id), "Advancer #1");
            Assert.That(advancingScores[1].Bowler.Id, Is.EqualTo(bowler3.Bowler.Id), "Advancer #2");
            Assert.That(advancingScores[2].Bowler.Id, Is.EqualTo(bowler4.Bowler.Id), "Advancer #3");
            Assert.That(advancingScores[3].Bowler.Id, Is.EqualTo(bowler5.Bowler.Id), "Advancer #4");
            Assert.That(advancingScores[4].Bowler.Id, Is.EqualTo(bowlerF.Bowler.Id), "Advancer #5");

            var cashingScores = result.CashingScores.ToList();
            Assert.That(cashingScores, Has.Count.EqualTo(3));
            Assert.That(cashingScores[0].Bowler.Id, Is.EqualTo(bowlerA.Bowler.Id), "Casher #1");
            Assert.That(cashingScores[1].Bowler.Id, Is.EqualTo(bowlerG.Bowler.Id), "Casher #2");
            Assert.That(cashingScores[2].Bowler.Id, Is.EqualTo(bowlerH.Bowler.Id), "Casher #3");

            Assert.That(result.NonQualifyingScores.Count(), Is.EqualTo(32));
        });
    }

    [Test]
    public void GitHubIssue9_Situation2()
    {
        var bowlerA = BuildBowlerSquadScore(300);
        var bowlerB = BuildBowlerSquadScore(299);
        var bowlerC = BuildBowlerSquadScore(298);
        var bowlerD = BuildBowlerSquadScore(-1);
        var bowlerE = BuildBowlerSquadScore(-2);

        var bowler6 = BuildBowlerSquadScore(35);
        var bowler7 = BuildBowlerSquadScore(34);
        var bowlerF = BuildBowlerSquadScore(33);
        var bowlerG = BuildBowlerSquadScore(32);
        var bowlerH = BuildBowlerSquadScore(31);

        var bowler11 = BuildBowlerSquadScore(30);
        var bowler12 = BuildBowlerSquadScore(29);
        var bowler13 = BuildBowlerSquadScore(28);
        var bowler14 = BuildBowlerSquadScore(27);
        var bowler15 = BuildBowlerSquadScore(26);

        var bowler16 = BuildBowlerSquadScore(25);
        var bowler17 = BuildBowlerSquadScore(24);
        var bowler18 = BuildBowlerSquadScore(23);
        var bowler19 = BuildBowlerSquadScore(22);
        var bowler20 = BuildBowlerSquadScore(21);

        var bowler21 = BuildBowlerSquadScore(20);
        var bowler22 = BuildBowlerSquadScore(19);
        var bowler23 = BuildBowlerSquadScore(18);
        var bowler24 = BuildBowlerSquadScore(17);
        var bowler25 = BuildBowlerSquadScore(16);

        var bowler26 = BuildBowlerSquadScore(15);
        var bowler27 = BuildBowlerSquadScore(14);
        var bowler28 = BuildBowlerSquadScore(13);
        var bowler29 = BuildBowlerSquadScore(12);
        var bowler30 = BuildBowlerSquadScore(11);

        var bowler31 = BuildBowlerSquadScore(10);
        var bowler32 = BuildBowlerSquadScore(9);
        var bowler33 = BuildBowlerSquadScore(8);
        var bowler34 = BuildBowlerSquadScore(7);
        var bowler35 = BuildBowlerSquadScore(6);

        var bowler36 = BuildBowlerSquadScore(5);
        var bowler37 = BuildBowlerSquadScore(4);
        var bowler38 = BuildBowlerSquadScore(3);
        var bowler39 = BuildBowlerSquadScore(2);
        var bowler40 = BuildBowlerSquadScore(1);

        var scores = new[] {bowlerA, bowlerB, bowlerC, bowlerD, bowlerE,
                            bowler6, bowler7, bowlerF, bowlerG, bowlerH,
                            bowler11, bowler12, bowler13, bowler14, bowler15,
                            bowler16, bowler17, bowler18, bowler19, bowler20,
                            bowler21, bowler22, bowler23, bowler24, bowler25,
                            bowler26, bowler27, bowler28, bowler29, bowler30,
                            bowler31, bowler32, bowler33, bowler34, bowler35,
                            bowler36, bowler37, bowler38, bowler39, bowler40 }.ToList();

        scores.Shuffle();

        var previousAdvancersIds = new[] { bowlerA.Bowler.Id, bowlerB.Bowler.Id, bowlerC.Bowler.Id, bowlerD.Bowler.Id, bowlerE.Bowler.Id };

        var cashRatio = 5.0m;
        var finalsRatio = 8.0m;

        var result = _calculator.Execute(SquadId.New(), new NortheastMegabuck.Models.Division(), scores, previousAdvancersIds, finalsRatio, cashRatio);

        Assert.Multiple(() =>
        {
            var advancingScores = result.AdvancingScores.ToList();
            Assert.That(advancingScores, Has.Count.EqualTo(5));
            Assert.That(advancingScores[0].Bowler.Id, Is.EqualTo(bowler6.Bowler.Id), "Advancer #1");
            Assert.That(advancingScores[1].Bowler.Id, Is.EqualTo(bowler7.Bowler.Id), "Advancer #2");
            Assert.That(advancingScores[2].Bowler.Id, Is.EqualTo(bowlerF.Bowler.Id), "Advancer #3");
            Assert.That(advancingScores[3].Bowler.Id, Is.EqualTo(bowlerG.Bowler.Id), "Advancer #4");
            Assert.That(advancingScores[4].Bowler.Id, Is.EqualTo(bowlerH.Bowler.Id), "Advancer #5");

            var cashingScores = result.CashingScores.ToList();
            Assert.That(cashingScores, Has.Count.EqualTo(3));
            Assert.That(cashingScores[0].Bowler.Id, Is.EqualTo(bowlerA.Bowler.Id), "Casher #1");
            Assert.That(cashingScores[1].Bowler.Id, Is.EqualTo(bowlerB.Bowler.Id), "Casher #2");
            Assert.That(cashingScores[2].Bowler.Id, Is.EqualTo(bowlerC.Bowler.Id), "Casher #3");

            Assert.That(result.NonQualifyingScores.Count(), Is.EqualTo(32));
        });
    }

    [Test]
    public void GitHubIssue9_Situation3()
    {
        var bowlerA = BuildBowlerSquadScore(300);
        var bowlerB = BuildBowlerSquadScore(299);
        var bowlerC = BuildBowlerSquadScore(298);
        var bowlerD = BuildBowlerSquadScore(297);
        var bowlerE = BuildBowlerSquadScore(296);

        var bowler6 = BuildBowlerSquadScore(35);
        var bowler7 = BuildBowlerSquadScore(34);
        var bowler8 = BuildBowlerSquadScore(33);
        var bowler9 = BuildBowlerSquadScore(32);
        var bowler10 = BuildBowlerSquadScore(31);

        var bowler11 = BuildBowlerSquadScore(30);
        var bowler12 = BuildBowlerSquadScore(29);
        var bowler13 = BuildBowlerSquadScore(28);
        var bowler14 = BuildBowlerSquadScore(27);
        var bowler15 = BuildBowlerSquadScore(26);

        var bowler16 = BuildBowlerSquadScore(25);
        var bowler17 = BuildBowlerSquadScore(24);
        var bowler18 = BuildBowlerSquadScore(23);
        var bowler19 = BuildBowlerSquadScore(22);
        var bowler20 = BuildBowlerSquadScore(21);

        var bowler21 = BuildBowlerSquadScore(20);
        var bowler22 = BuildBowlerSquadScore(19);
        var bowler23 = BuildBowlerSquadScore(18);
        var bowler24 = BuildBowlerSquadScore(17);
        var bowler25 = BuildBowlerSquadScore(16);

        var bowler26 = BuildBowlerSquadScore(15);
        var bowler27 = BuildBowlerSquadScore(14);
        var bowler28 = BuildBowlerSquadScore(13);
        var bowler29 = BuildBowlerSquadScore(12);
        var bowler30 = BuildBowlerSquadScore(11);

        var bowler31 = BuildBowlerSquadScore(10);
        var bowler32 = BuildBowlerSquadScore(9);
        var bowler33 = BuildBowlerSquadScore(8);
        var bowler34 = BuildBowlerSquadScore(7);
        var bowler35 = BuildBowlerSquadScore(6);

        var bowler36 = BuildBowlerSquadScore(5);
        var bowler37 = BuildBowlerSquadScore(4);
        var bowler38 = BuildBowlerSquadScore(3);
        var bowler39 = BuildBowlerSquadScore(2);
        var bowler40 = BuildBowlerSquadScore(1);

        var scores = new[] {bowlerA, bowlerB, bowlerC, bowlerD, bowlerE,
                            bowler6, bowler7, bowler8, bowler9, bowler10,
                            bowler11, bowler12, bowler13, bowler14, bowler15,
                            bowler16, bowler17, bowler18, bowler19, bowler20,
                            bowler21, bowler22, bowler23, bowler24, bowler25,
                            bowler26, bowler27, bowler28, bowler29, bowler30,
                            bowler31, bowler32, bowler33, bowler34, bowler35,
                            bowler36, bowler37, bowler38, bowler39, bowler40 }.ToList();

        scores.Shuffle();

        var previousAdvancersIds = new[] { bowlerA.Bowler.Id, bowlerB.Bowler.Id, bowlerC.Bowler.Id, bowlerD.Bowler.Id, bowlerE.Bowler.Id };

        var cashRatio = 5.0m;
        var finalsRatio = 8.0m;

        var result = _calculator.Execute(SquadId.New(), new NortheastMegabuck.Models.Division(), scores, previousAdvancersIds, finalsRatio, cashRatio);

        Assert.Multiple(() =>
        {
            var advancingScores = result.AdvancingScores.ToList();
            Assert.That(advancingScores, Has.Count.EqualTo(5));
            Assert.That(advancingScores[0].Bowler.Id, Is.EqualTo(bowler6.Bowler.Id), "Advancer #1");
            Assert.That(advancingScores[1].Bowler.Id, Is.EqualTo(bowler7.Bowler.Id), "Advancer #2");
            Assert.That(advancingScores[2].Bowler.Id, Is.EqualTo(bowler8.Bowler.Id), "Advancer #3");
            Assert.That(advancingScores[3].Bowler.Id, Is.EqualTo(bowler9.Bowler.Id), "Advancer #4");
            Assert.That(advancingScores[4].Bowler.Id, Is.EqualTo(bowler10.Bowler.Id), "Advancer #5");

            var cashingScores = result.CashingScores.ToList();
            Assert.That(cashingScores, Has.Count.EqualTo(5));
            Assert.That(cashingScores[0].Bowler.Id, Is.EqualTo(bowlerA.Bowler.Id), "Casher #1");
            Assert.That(cashingScores[1].Bowler.Id, Is.EqualTo(bowlerB.Bowler.Id), "Casher #2");
            Assert.That(cashingScores[2].Bowler.Id, Is.EqualTo(bowlerC.Bowler.Id), "Casher #3");
            Assert.That(cashingScores[3].Bowler.Id, Is.EqualTo(bowlerD.Bowler.Id), "Casher #4");
            Assert.That(cashingScores[4].Bowler.Id, Is.EqualTo(bowlerE.Bowler.Id), "Casher #5");

            Assert.That(result.NonQualifyingScores.Count(), Is.EqualTo(30));
        });
    }
}

internal static class Extensions
{
    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = RandomNumberGenerator.GetInt32(n+1);
            (list[n], list[k]) = (list[k], list[n]);
        }
    }
}