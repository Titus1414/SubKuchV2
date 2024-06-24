#include<iostream>
#include<conio.h>
using namespace std;

class A
{
	public:
		virtual void show()
		{
			cout<<"Parent Class A"<<endl;
		}
	
};
class B : public A
{
	public:
		void show()
		{
			cout<<"Child Class B"<<endl;
		}
};
class C : public A
{
	public:
		void show()
		{
			cout<<"Child Class C"<<endl;
		}
};
int main()
{
	A obj1;
	B obj2;
	C obj3;
	
	obj1.show();
	obj2.show();
	obj3.show();
	
	return 0;
}


