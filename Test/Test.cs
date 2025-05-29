using Xunit;
namespace ADODISHES.Test
{
	public class Test
	{
		[Theory]
		[InlineData(1, 1, 2)]
		[InlineData(2, -2, 0)]
		[InlineData(3, -4, -1)]
		[InlineData(-4, -4, -8)]

		public void Test1(int a , int b, int sum)
		{
		{
			Assert.Equal(a+b,sum);
		}
		}


		[Fact]
		public void Test2()
		{
			Assert.True(true);
		}
	}
}
